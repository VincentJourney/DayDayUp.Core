using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;

namespace Autofac_MediatR
{
    public class CustomHandler : AsyncRequestHandler<CustomRequest>
    {
        protected override async Task Handle(CustomRequest request, CancellationToken cancellationToken)
        {
            Thread.Sleep(5000);
            System.Console.WriteLine(request.a);
        }
    }
}
