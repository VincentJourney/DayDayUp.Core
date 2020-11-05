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
    class Program
    {
        static void Main(string[] args)
        {
            AutofacContainer.Build();
            IPeople p = AutofacContainer.Instance.Resolve<IPeople>();
            p.Run();
            Console.ReadKey();
        }

    }
}
