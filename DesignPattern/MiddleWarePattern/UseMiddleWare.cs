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
        //public static void MiddleWareAnalysis()
        //{
        //    Console.WriteLine(" ** 模拟 Middleware **");
        //    var context = new RequestContext() { name = "xx", id = 1 };

        //    MiddleWareBuild middleWareEvent = new MiddleWareBuild();
        //    //PipeLineExtensions.ActionExcutingEvent += c => middleWareEvent.ActionExcutingEventHanlder(c);
        //    //PipeLineExtensions.ActionExcutedEvent += c => middleWareEvent.ActionExcutedEventHanlder(c);
        //    //PipeLineExtensions.ExceptionEvent += c => middleWareEvent.ExceptionEventHanlder(c);

        //    IList<Func<Action<RequestContext>, Action<RequestContext>>> _pipeline
        //       = new List<Func<Action<RequestContext>, Action<RequestContext>>>();
        //    Action<RequestContext, Action> ac1 = (context, next) =>
        //    {
        //        Console.WriteLine("1 Before");
        //        next();
        //        Console.WriteLine("1 After");
        //    };
        //    Action<RequestContext, Action> ac2 = (context, next) =>
        //    {
        //        Console.WriteLine("2 Before");
        //        next();
        //        Console.WriteLine("2 After");
        //    };

        //    Func<Action<RequestContext>, Action<RequestContext>> func1 = (next) =>
        //    context =>
        //    ac1(context, () => { next(context); });
        //    _pipeline.Add(func1);

        //    Func<Action<RequestContext>, Action<RequestContext>> func2 = (next) =>
        //    context =>
        //    ac2(context, () => next(context));
        //    _pipeline.Add(func2);

        //    Action<RequestContext> ac = context => Console.WriteLine($"{nameof(context.name)} {context.name}");
        //    foreach (var item in _pipeline.Reverse())
        //        ac = item(ac);
        //    ac(context);
        //}

        public static void BuildUse()
        {

            Console.WriteLine(" *** Use MiddleWare ***");
            var context = new RequestContext<CheckProductMode>() { Mode = new CheckProductMode { AliexpressRule = "1" } };

            //MiddleWareBuild middleWareEvent = new MiddleWareBuild();
            //PipeLineExtensions.ActionExcutingEvent += c => middleWareEvent.ActionExcutingEventHanlder(c);
            //PipeLineExtensions.ActionExcutedEvent += c => middleWareEvent.ActionExcutedEventHanlder(c);
            //PipeLineExtensions.ExceptionEvent += c => middleWareEvent.ExceptionEventHanlder(c);

            var builder = PipelineBuilder<RequestContext<CheckProductMode>>.Create(context =>
            {
               
            });
            builder.UseMiddleware<RequestContext<CheckProductMode>, CustomExceptionMiddleWare<RequestContext<CheckProductMode>>>();
            builder.UseMiddleware<RequestContext<CheckProductMode>, CustomMiddleWare>();
            builder.UseMiddleware<RequestContext<CheckProductMode>, CustomMiddleWare2>();

            var app = builder.Build();

            app(context);
            Console.WriteLine(context.ExceptionInfo.Message);

            Console.ReadLine();
        }

    }
    public class RequestContext<T>
    {
        public MiddleWareExceptionInfo ExceptionInfo { get; set; }
        public T Mode { get; set; }
    }
    public class CheckProductMode
    {
        public string AliexpressRule { get; set; }
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
