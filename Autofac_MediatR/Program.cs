using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using Serilog;
using Serilog.Events;

namespace Autofac_MediatR
{
    class Program
    {
        static void Main(string[] args)
        {
            IPeople people = AutofacContainer.Instance.Resolve<IPeople>();
            people.Run();
            Console.ReadKey();
        }
    }
}
