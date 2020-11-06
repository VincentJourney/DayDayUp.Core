using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using MediatR;

namespace Autofac_MediatR
{
    public static class MediatRMiddleWare
    {
        /// <summary>
        /// 注入MediatR
        /// </summary>
        /// <param name="builder">ContainerBuilder</param>
        /// <returns></returns>
        public static ContainerBuilder AddMediatR(this ContainerBuilder builder)
        {
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.TryResolve(t, out var o) ? o : null;
            }).InstancePerLifetimeScope();

            builder.Register<Func<IEnumerable<Task>, Task>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return async tasks =>
                {
                    foreach (Task task in tasks)
                    {
                        await task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                };
            });

            builder.RegisterType<AsyncPublisher>().SingleInstance();
            builder.RegisterType<CustomMediator>().As<IMediator>();
            builder.RegisterType<Mediator>().As<IMediator>();

            builder.RegisterAssemblyTypes(typeof(Program).GetTypeInfo().Assembly).AsImplementedInterfaces();

            return builder;
        }
    }
}
