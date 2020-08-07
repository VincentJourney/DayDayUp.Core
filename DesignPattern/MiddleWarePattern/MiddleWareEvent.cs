using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.MiddleWarePattern
{
    public abstract class MiddleWareEvent
    {
        public virtual void ActionExcutingEventHanlder(object obj)
        => Console.WriteLine("Excuting");

        public virtual void ActionExcutedEventHanlder(object obj)
        => Console.WriteLine("Excuted");
        public virtual void ExceptionEventHanlder(object obj)
        => Console.WriteLine("Exception");
    }
}
