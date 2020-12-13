using MediatR;

namespace Autofac_MediatR
{
    public class CustomRequest : IRequest
    {
        public int a { get; set; }
    }
}
