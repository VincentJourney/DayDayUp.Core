using System;
using System.Reflection;
using Autofac;
using AutofacSerilogIntegration;
using MediatR;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Autofac_MediatR
{
    /// <summary>
    /// 控制台程序容器
    /// </summary>
    public static class AutofacContainer
    {
        private readonly static Object buildLock = new Object();
        static AutofacContainer()
        {
            if (Instance == null)
            {
                lock (buildLock)
                {
                    if (Instance == null)
                    {
                        Instance = new ContainerBuilder().AddCustomModule().Build();
                    }
                }
            }
        }
        /// <summary>
        /// 容器
        /// </summary>
        public static IContainer Instance;

        /// <summary>
        /// 注入自定义模块
        /// </summary>
        /// <param name="builder">ContainerBuilder</param>
        private static ContainerBuilder AddCustomModule(this ContainerBuilder builder)
        => builder.AddMediatR()
                .AddBussinessService()
                .AddSerilog();
    }
}
