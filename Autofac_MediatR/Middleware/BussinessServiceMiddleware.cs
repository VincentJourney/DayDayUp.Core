using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Autofac_MediatR
{
    public static class BussinessServiceMiddleware
    {
        /// <summary>
        /// 注入业务服务
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder AddBussinessService(this ContainerBuilder builder)
        {
            builder.RegisterType<Student>().As<IStudent>();
            builder.RegisterType<People>().As<IPeople>();
            return builder;
        }
    }
}
