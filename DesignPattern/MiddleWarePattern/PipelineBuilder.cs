using DesignPattern.MiddleWarePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
                    //try
                    //{
                    ActionExcutingEvent?.Invoke(context);
                    next(context);
                    ActionExcutedEvent?.Invoke(context);
                    //}
                    //catch (Exception ex)
                    //{
                    //    ExceptionEvent?.Invoke(ex.Message);
                    //}
                })
                );
            return build;
        }
        public static IPipelineBuilder<TContext> UseMiddleware<TContext, T>(this IPipelineBuilder<TContext> builder) where T : class
        {
            //var _pipelines = builder.GetType()
            //    .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            //    .Where(s => s.Name == "_pipelines").FirstOrDefault();
            //var value = _pipelines.GetValue(builder) as List<Func<Action<TContext>, Action<TContext>>>;
            //var lastfunc = value.Last();
            var build = builder.Use(next =>
                context =>
                {
                    var MiddleWare = typeof(T);
                    var Constructor = MiddleWare.GetConstructors().FirstOrDefault();
                    var ConstructorPara = Constructor.GetParameters().Count();
                    if (Constructor == null || ConstructorPara == 0)
                        throw new Exception($"{MiddleWare.Name} has not Constructor");

                    var instance = Constructor.Invoke(new object[] { next }) as T;

                    var instanceInvokeMethod = instance.GetType().GetMethods().Where(s => s.Name == "Invoke").FirstOrDefault();
                    if (instanceInvokeMethod == null)
                        throw new Exception($"{MiddleWare.Name} has not Constructor");

                    instanceInvokeMethod.Invoke(instance, new object[] { context });
                });
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



    }
}
