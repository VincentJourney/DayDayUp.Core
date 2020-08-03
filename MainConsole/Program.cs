using DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MainConsole
{
    class Program
    {

        public delegate Action<RequestContext> RequestDelegete();
        static void Main(string[] args)
        {
            var context = new RequestContext() { name = "xx", id = 1 };
            IList<Func<Action<RequestContext>, Action<RequestContext>>> _pipeline
               = new List<Func<Action<RequestContext>, Action<RequestContext>>>();
            Action<RequestContext, Action> ac1 = (context, next) =>
            {
                Console.WriteLine("1 Before");
                next();
                Console.WriteLine("1 After");
            };
            Action<RequestContext, Action> ac2 = (context, next) =>
            {
                Console.WriteLine("2 Before");
                next();
                Console.WriteLine("2 After");
            };

            Func<Action<RequestContext>, Action<RequestContext>> func1 = (next) =>
            context =>
            ac1(context, () => next(context));
            _pipeline.Add(func1);

            Func<Action<RequestContext>, Action<RequestContext>> func2 = (next) =>
            context =>
            ac2(context, () => next(context));
            _pipeline.Add(func2);

            Action<RequestContext> ac = context => Console.WriteLine($"{nameof(context.name)} {context.name}");
            foreach (var item in _pipeline.Reverse())
                ac = item(ac);
            ac(context);
            Console.ReadKey();

            var builder = PipelineBuilder<RequestContext>.Create(context =>
            {
                Console.WriteLine($"{nameof(context.name)} {context.name} {nameof(context.id)} {context.id}");
            })
                .Use((context, next) =>
                {
                    Console.WriteLine("1 Before");
                    next();
                    Console.WriteLine("1 After");
                })
                 .Use((context, next) =>
                 {
                     Console.WriteLine("2 Before");
                     next();
                     Console.WriteLine("2 After");
                 })
               // .Run((context) =>
               //{
               //    Console.WriteLine("3 Before");
               //    // next();
               //    Console.WriteLine("3 After");
               //}).Use((context, next) =>
               //{
               //    Console.WriteLine("4 Before");
               //    // next();
               //    Console.WriteLine("4 After");
               //})
               ;

            var app = builder.Build();

            app(context);

        }


    }

    public class RequestContext
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
