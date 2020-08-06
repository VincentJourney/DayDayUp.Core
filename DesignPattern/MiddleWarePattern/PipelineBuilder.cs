using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    public interface IPipelineBuilder<TContext>
    {
        IPipelineBuilder<TContext> Use(Func<Action<TContext>, Action<TContext>> middleware);
        Action<TContext> Build();
        IPipelineBuilder<TContext> New();
    }

    public class PipelineBuilder<TContext> : IPipelineBuilder<TContext>
    {
        private readonly Action<TContext> _completeFunc;
        private readonly IList<Func<Action<TContext>, Action<TContext>>> _pipelines = new List<Func<Action<TContext>, Action<TContext>>>();

        public PipelineBuilder(Action<TContext> completeFunc) => _completeFunc = completeFunc;

        public Action<TContext> Build()
        {
            var request = _completeFunc;
            foreach (var pipeline in _pipelines.Reverse())
                request = pipeline(request);
            return request;
        }

        public static IPipelineBuilder<TContext> Create(Action<TContext> completeAction) =>
            new PipelineBuilder<TContext>(completeAction);

        public IPipelineBuilder<TContext> New() => new PipelineBuilder<TContext>(_completeFunc);

        public IPipelineBuilder<TContext> Use(Func<Action<TContext>, Action<TContext>> middleware)
        {
            _pipelines.Add(middleware);
            return this;
        }
    }

    public static class PipeLineExtensions
    {
        public static event Action<object> ActionExcutingEvent;

        public static event Action<object> ActionExcutedEvent;

        public static event Action<object> ExceptionEvent;
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext, Action> action)
        {
            var build = builder.Use(next =>
                context => action(context, () =>
                {
                    try
                    {
                        ActionExcutingEvent?.Invoke(context);
                        next(context);
                        ActionExcutedEvent?.Invoke(context);
                    }
                    catch (Exception ex)
                    {
                        ExceptionEvent?.Invoke(ex.Message);
                    }
                })
                );
            return build;
        }

        public static IPipelineBuilder<TContext> Run<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext> action)
        => builder.Use(_ => action);

        public static IPipelineBuilder<TContext> When<TContext>(this IPipelineBuilder<TContext> builder, Func<TContext, bool> predict, Action<IPipelineBuilder<TContext>> configureAction)
        {
            return builder.Use((context, next) =>
            {
                if (predict.Invoke(context))
                {
                    var branchPipelineBuilder = builder.New();
                    configureAction(branchPipelineBuilder);
                    var branchPipeline = branchPipelineBuilder.Build();
                    branchPipeline.Invoke(context);
                }
                else
                {
                    next();
                }
            });
        }

        //public static IPipelineBuilder<TContext> UseMiddleware(this IPipelineBuilder<TContext> builder)
        //{


        //}
    }
}
