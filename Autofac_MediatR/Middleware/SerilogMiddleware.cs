using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using Serilog.Events;

namespace Autofac_MediatR
{
    public static class SerilogMiddleware
    {
        /// <summary>
        /// 注入Serilog
        /// </summary>
        /// <param name="builder">ContainerBuilder</param>
        /// <returns></returns>
        public static ContainerBuilder AddSerilog(this ContainerBuilder builder)
        {
            builder.Register<ILogger>((c, p) =>
             new LoggerConfiguration()
                  .WriteToFileConfiguration()
                  .CreateLogger()
            ).SingleInstance();
            return builder;
        }

        /// <summary>
        /// 日志写入文件配置项
        /// </summary>
        /// <param name="logger">LoggerConfiguration</param>
        /// <param name="outputTemplate">日志模板</param>
        /// <returns></returns>
        private static LoggerConfiguration WriteToFileConfiguration(this LoggerConfiguration logger, string outputTemplate = "[{Timestamp:HH:mm:ss.FFF} {Level}] {Message} {NewLine}{Exception}")
        => logger.WriteToFileByLevelConfiguration(logEventLevel: LogEventLevel.Information, outputTemplate)
            .WriteToFileByLevelConfiguration(logEventLevel: LogEventLevel.Warning, outputTemplate)
            .WriteToFileByLevelConfiguration(logEventLevel: LogEventLevel.Error, outputTemplate)
            .WriteToFileByLevelConfiguration(logEventLevel: LogEventLevel.Fatal, outputTemplate);

        /// <summary>
        /// 日志写入文件日志级别过滤配置项
        /// </summary>
        /// <param name="logger">LoggerConfiguration</param>
        /// <param name="logEventLevel">日志级别</param>
        /// <param name="outputTemplate">日志输出模板</param>
        /// <returns></returns>
        private static LoggerConfiguration WriteToFileByLevelConfiguration(this LoggerConfiguration logger, LogEventLevel logEventLevel, string outputTemplate)
        => logger.WriteTo.Logger(lg => lg.Filter
                   .ByIncludingOnly(info => info.Level == logEventLevel)
                   .WriteTo.File(GetLogFilePathByLogLevel(logEventLevel),
                   outputTemplate: outputTemplate,
                   retainedFileCountLimit: 30,
                   shared: true));

        /// <summary>
        /// 根据日志级别获取路径
        /// </summary>
        /// <param name="logEventLevel">日志级别</param>
        /// <returns></returns>
        private static string GetLogFilePathByLogLevel(LogEventLevel logEventLevel)
        {
            var datetimeNow = DateTime.Now;
            return $"Log/{logEventLevel.ToString()}/{datetimeNow.Year}-{datetimeNow.Month}/Day{datetimeNow.Day}/Hour_{datetimeNow.Hour}.log";
        }
    }
}
