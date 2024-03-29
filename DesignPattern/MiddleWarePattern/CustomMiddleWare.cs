﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilty;

namespace DesignPattern.MiddleWarePattern
{
    public class CustomMiddleWare
    {
        private readonly Action<RequestContext<CheckProductMode>> _next;
        public CustomMiddleWare(Action<RequestContext<CheckProductMode>> next)
        {
            _next = next;
        }
        public void Invoke(RequestContext<CheckProductMode> context)
        {
            Console.WriteLine("CustomMiddleWare Before");

            if (context.Mode.AliexpressRule != "1")
            {
                _next(context);
                return;
            }

            _next(context);
            return;

            Console.WriteLine("CustomMiddleWare After");
        }
    }

    public class CustomMiddleWare2
    {
        private readonly Action<RequestContext<CheckProductMode>> _next;
        public CustomMiddleWare2(Action<RequestContext<CheckProductMode>> next)
        {
            _next = next;
        }
        public void Invoke(RequestContext<CheckProductMode> context)
        {
            Console.WriteLine("CustomMiddleWare2 Before");

            if (context.Mode.AliexpressRule == "1")
            {
                //_next(context);
                return;
            }
            _next(context);

            Console.WriteLine("CustomMiddleWare2 After");
        }
    }


    public class CustomMiddleWare3
    {
        private readonly Action<RequestContext<CheckProductMode>> _next;
        public CustomMiddleWare3(Action<RequestContext<CheckProductMode>> next)
        {
            _next = next;
        }
        public void Invoke(RequestContext<CheckProductMode> context)
        {
            Console.WriteLine("CustomMiddleWare2 Before");

            if (context.Mode.AliexpressRule != "1")
            {
                _next(context);
                return;
            }
            _next(context);

            Console.WriteLine("CustomMiddleWare2 After");
        }
    }

    /// <summary>
    /// 通用异常类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomExceptionMiddleWare<T>
    {
        private readonly Action<T> _next;
        public CustomExceptionMiddleWare(Action<T> next) => _next = next;

        public void Invoke(T context)
        {
            try
            {
                _next(context);
            }
            catch (BException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                var info = GetMiddleWareException(ex);
                var Property = context.GetType().GetProperty("ExceptionInfo");
                Property?.SetValue(context, info);
                this.exceptionMessage.Clear();
                exceptionInfo = new MiddleWareExceptionInfo();
            }
        }

        private MiddleWareExceptionInfo GetMiddleWareException(Exception ex)
        {
            if (ex == null)
                return exceptionInfo;

            this.exceptionMessage.Add(ex.Message);
            if (ex is BException)
            {
                this.exceptionInfo.ErrorCode = 2;
                this.exceptionInfo.Message = this.exceptionMessage.LastOrDefault();
                return exceptionInfo;
            }
            else
            {
                this.exceptionInfo.ErrorCode = 1;
                this.exceptionInfo.Message = string.Join(",", this.exceptionMessage);
            }

            this.GetMiddleWareException(ex?.InnerException);
            return exceptionInfo;
        }
        private List<string> exceptionMessage = new List<string>();
        private MiddleWareExceptionInfo exceptionInfo = new MiddleWareExceptionInfo();

    }
    /// <summary>
    /// 中间件异常
    /// </summary>
    public class MiddleWareExceptionInfo
    {
        public string Message { get; set; }
        /// <summary>
        /// 1 为系统级别异常  2 业务异常
        /// </summary>
        public int ErrorCode { get; set; }
    }
}
