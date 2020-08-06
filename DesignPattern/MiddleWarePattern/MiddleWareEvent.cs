using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.MiddleWarePattern
{
    public abstract class MiddleWareEvent
    {
        public virtual void ActionExcutingEventHanlder(object obj)
        {

        }
        public virtual void ActionExcutedEventHanlder(object obj)
        {

        }
        public virtual void ExceptionEventHanlder(object obj)
        {

        }
    }
}
