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
            throw new System.Exception("send Handle Error");
            Thread.Sleep(5000);
            System.Console.WriteLine(request.a);
            await Task.FromResult("111111111111");
        }
    }
}
