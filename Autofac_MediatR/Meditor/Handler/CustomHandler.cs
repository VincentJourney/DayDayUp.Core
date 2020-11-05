using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;

namespace Autofac_MediatR
{
    
    public class CustomHandler2 : IRequestHandler<CustomRequest, string>
    {
        public Task<string> Handle(CustomRequest request, CancellationToken cancellationToken)
        {
            System.Console.WriteLine(request.a);
            return Task.FromResult("2222222222222222");
        }
    }
    public class CustomHandler : IRequestHandler<CustomRequest, string>
    {
        public Task<string> Handle(CustomRequest request, CancellationToken cancellationToken)
        {
            System.Console.WriteLine(request.a);
            return Task.FromResult("111111111111");
        }
    }
}
