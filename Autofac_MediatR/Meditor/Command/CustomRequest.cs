using MediatR;

namespace Autofac_MediatR
{
    public class CustomRequest : IRequest<string>
    {
        public string a { get; set; }
    }
}
