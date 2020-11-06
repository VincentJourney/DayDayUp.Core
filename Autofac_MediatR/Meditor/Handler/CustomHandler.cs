using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;

namespace Autofac_MediatR
{

    //public class CustomHandler2 : IRequestHandler<CustomRequest, string>
    //{
    //    public Task<string> Handle(CustomRequest request, CancellationToken cancellationToken)
    //    {
    //        System.Console.WriteLine(request.a);
    //        return Task.FromResult("2222222222222222");
    //    }
    //}
    public class CustomHandler : AsyncRequestHandler<CustomRequest>
    {
        //public Task<string> Handle(CustomRequest request, CancellationToken cancellationToken)
        //{
        //    Thread.Sleep(5000);
        //    return Task.FromResult("111111111111");
        //}

        protected override async Task Handle(CustomRequest request, CancellationToken cancellationToken)
        {
            throw new System.Exception("send Handle Error");
            Thread.Sleep(5000);
            System.Console.WriteLine(request.a);
            await Task.FromResult("111111111111");
        }
    }
}
