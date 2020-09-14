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
        /// <summary>
        /// 添加中间件
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IPipelineBuilder<TContext> Use<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext, Action> action)
        {
            var build = builder.Use(next => context => action(context, () => next(context)));
            return build;
        }
        /// <summary>
        /// 自定义中间件，约定类中需要有invoke的方法
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IPipelineBuilder<TContext> UseMiddleware<TContext, T>(this IPipelineBuilder<TContext> builder) where T : class
        {
            var build = builder.Use(next =>
                context =>
                {
                    var MiddleWare = typeof(T);
                    var Constructor = MiddleWare.GetConstructors().FirstOrDefault();
                    if (Constructor == null)
                        throw new Exception($"{MiddleWare.Name} has not Constructor");
                    var ConstructorPara = Constructor.GetParameters().Count();
                    if (ConstructorPara == 0)
                        throw new Exception($"{MiddleWare.Name} ConstructorParams Num is 0");
                    //可在构造函数加入参数
                    var instance = Constructor.Invoke(new object[] { next }) as T;
                    var instanceInvokeMethod = instance.GetType().GetMethods().Where(s => s.Name == "Invoke").FirstOrDefault();
                    if (instanceInvokeMethod == null)
                        throw new Exception($"{MiddleWare.Name} has not Invoke Method");

                    instanceInvokeMethod.Invoke(instance, new object[] { context });
                });
            return build;
        }

        /// <summary>
        /// 终结点中间件
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IPipelineBuilder<TContext> Run<TContext>(this IPipelineBuilder<TContext> builder, Action<TContext> action)
        => builder.Use(_ => action);
    }
}
