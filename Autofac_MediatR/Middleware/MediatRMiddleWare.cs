using System.Reflection;
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
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.TryResolve(t, out var o) ? o : null;
            }).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(Program).GetTypeInfo().Assembly).AsImplementedInterfaces();
            return builder;
        }
    }
}
