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
            Task.Delay(3000);
            Console.WriteLine($"{notification.MsgId},{Thread.CurrentThread.ManagedThreadId}");
        }
    }

    public class CustomNotificationHandler2 : INotificationHandler<CustomNotification>
    {
        public async Task Handle(CustomNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{notification.MsgId},{Thread.CurrentThread.ManagedThreadId}");
        }
    }
    public class CustomNotificationHandler3 : INotificationHandler<CustomNotification>
    {
        public async Task Handle(CustomNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{notification.MsgId},{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
