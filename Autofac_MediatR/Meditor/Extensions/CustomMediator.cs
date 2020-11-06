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

    public static class IMediatorExtionsens
    {
        public static async Task SendAsync<TResponse>(this IMediator mediator, IEnumerable<IRequest<TResponse>> requests, CancellationToken cancellationToken = default(CancellationToken), bool async = false)
        {
            try
            {


                if (async)
                {
                    Parallel.ForEach(requests, async request =>
                    {
                        await mediator.Send(request, cancellationToken);
                    });
                }
                else
                {
                    foreach (var request in requests)
                    {
                        await mediator.Send(request, cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
