using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace DesignPattern.MiddleWarePattern
{
    public class UseMiddleWare
    {
        public static void MiddleWareAnalysis()
        {
            Console.WriteLine(" ** 模拟 Middleware **");
            var context = new RequestContext() { name = "xx", id = 1 };

            MiddleWareBuild middleWareEvent = new MiddleWareBuild();
            Action<object> action = (a) => middleWareEvent.ActionExcutingEventHanlder(a);
            PipeLineExtensions.ActionExcutingEvent += action;
            PipeLineExtensions.ActionExcutedEvent += c => middleWareEvent.ActionExcutedEventHanlder(c);
            PipeLineExtensions.ExceptionEvent += c => middleWareEvent.ExceptionEventHanlder(c);

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
            ac1(context, () => { next(context); });
            _pipeline.Add(func1);

            Func<Action<RequestContext>, Action<RequestContext>> func2 = (next) =>
            context =>
            ac2(context, () => next(context));
            _pipeline.Add(func2);

            Action<RequestContext> ac = context => Console.WriteLine($"{nameof(context.name)} {context.name}");
            foreach (var item in _pipeline.Reverse())
                ac = item(ac);
            ac(context);
        }

        public static void BuildUse()
        {

            Console.WriteLine(" *** Use MiddleWare ***");
            var context = new RequestContext() { name = "xx", id = 1 };

            //MiddleWareBuild middleWareEvent = new MiddleWareBuild();
            //PipeLineExtensions.ActionExcutingEvent += c => middleWareEvent.ActionExcutingEventHanlder(c);
            //PipeLineExtensions.ActionExcutedEvent += c => middleWareEvent.ActionExcutedEventHanlder(c);
            //PipeLineExtensions.ExceptionEvent += c => middleWareEvent.ExceptionEventHanlder(c);

            Action<RequestContext> action = context => Console.WriteLine($"MainFunc {JsonConvert.SerializeObject(context)}");

            var builder = PipelineBuilder<RequestContext>.Create(action);

            builder.UseMiddleware<RequestContext, CustomExceptionMiddleWare>();
            builder.Use((context, next) =>
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
            //.Run(context =>
            //{
            //    Console.WriteLine("终结点");
            //});
            ;
            //builder.UseMiddleware<RequestContext, CustomMiddleWare>();
            //builder.UseMiddleware<RequestContext, CustomMiddleWare2>();

            var app = builder.Build();
            app(context);
            Console.ReadLine();
        }

    }
    public class RequestContext
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class MiddleWareBuild : MiddleWareEvent
    {
        public override void ActionExcutedEventHanlder(object obj)
        {
            Console.WriteLine("ExcutedEvent  =>  " + JsonConvert.SerializeObject(obj));
            base.ActionExcutedEventHanlder(obj);
        }

        public override void ActionExcutingEventHanlder(object obj)
        {
            Console.WriteLine("ExcutingEvent  =>  " + JsonConvert.SerializeObject(obj));
            base.ActionExcutingEventHanlder(obj);
        }

        public override void ExceptionEventHanlder(object obj)
        {
            Console.WriteLine("ExceptionEvent  =>  " + obj.ToString());
            base.ExceptionEventHanlder(obj);
        }
    }
}
