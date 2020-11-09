using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Autofac_MediatR
{
    public class CustomNotificationHandler : INotificationHandler<CustomNotification>
    {
        public async Task Handle(CustomNotification notification, CancellationToken cancellationToken)
        {
            throw new Exception("嘻嘻1");
            await Task.Delay(3000);
            Console.WriteLine($"{notification.MsgId},{Thread.CurrentThread.ManagedThreadId}");
        }
    }

    public class CustomNotificationHandler2 : INotificationHandler<CustomNotification>
    {
        public async Task Handle(CustomNotification notification, CancellationToken cancellationToken)
        {
            await Task.Delay(3000);
            Console.WriteLine($"{notification.MsgId},{Thread.CurrentThread.ManagedThreadId}");
        }
    }
    public class CustomNotificationHandler3 : INotificationHandler<CustomNotification>
    {
        public async Task Handle(CustomNotification notification, CancellationToken cancellationToken)
        {
            throw new Exception("嘻嘻3");
            await Task.Delay(3000);
            Console.WriteLine($"{notification.MsgId},{Thread.CurrentThread.ManagedThreadId}");
        }
    }
    public class CustomNotificationHandler4 : INotificationHandler<CustomNotification>
    {
        public async Task Handle(CustomNotification notification, CancellationToken cancellationToken)
        {
            await Task.Delay(3000);
            Console.WriteLine($"{notification.MsgId},{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
