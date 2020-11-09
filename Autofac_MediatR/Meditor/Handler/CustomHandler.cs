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
            throw new System.Exception("1");
            Thread.Sleep(2000);
            System.Console.WriteLine(request.a);
        }
    }
}
