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
    public class CustomExceptionMiddleWare
    {
        private readonly Action<RequestContext> _next;
        public CustomExceptionMiddleWare(Action<RequestContext> next)
        {
            _next = next;
        }
        public void Invoke(RequestContext context)
        {
            try
            {
                Console.WriteLine("CustomExceptionMiddleWare before");
                _next(context);
                Console.WriteLine("CustomExceptionMiddleWare after");
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"CustomExceptionMiddleWare  =>  {this.FindAllException(ex)}");
            }
        }

        public string Message = string.Empty;
        public string FindAllException(Exception ex)
        {
            if (ex == null)
                return string.Empty;
            this.Message += $" Message is [{ex.Message}]";
            if (ex.InnerException != null)
                this.FindAllException(ex.InnerException);

            return this.Message;
        }
    }
}
