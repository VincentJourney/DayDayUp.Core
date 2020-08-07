using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.MiddleWarePattern
{
    public class CustomMiddleWare
    {
        private readonly Action<RequestContext> _next;
        public CustomMiddleWare(Action<RequestContext> next)
        {
            _next = next;
        }
        public void Invoke(RequestContext context)
        {
            Console.WriteLine("CustomMiddleWare Before");
            _next(context);
            Console.WriteLine("CustomMiddleWare After");
        }
    }

    public class CustomMiddleWare2
    {
        private readonly Action<RequestContext> _next;
        public CustomMiddleWare2(Action<RequestContext> next)
        {
            _next = next;
        }
        public void Invoke(RequestContext context)
        {
            Console.WriteLine("CustomMiddleWare2 Before");
            _next(context);
            Console.WriteLine("CustomMiddleWare2 After");
        }
    }
}
