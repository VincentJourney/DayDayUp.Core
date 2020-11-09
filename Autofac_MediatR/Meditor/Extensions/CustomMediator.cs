using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Autofac_MediatR
{
    public class CustomMediator : Mediator
    {
        private readonly Func<IEnumerable<Task>, Task> _publish;

        public CustomMediator(ServiceFactory serviceFactory, Func<IEnumerable<Task>, Task> publish) : base(serviceFactory)
        => _publish = publish;

        protected override Task PublishCore(IEnumerable<Task> allHandlers)
        => _publish(allHandlers);
    }
}
