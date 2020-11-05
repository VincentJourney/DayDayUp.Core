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
        /// <summary>
        /// 容器
        /// </summary>
        public static IContainer Instance;

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <returns></returns>
        public static void Build()
        => Instance = new ContainerBuilder().AddCustomModule().Build();

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
