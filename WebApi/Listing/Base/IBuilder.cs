using System.Collections.Generic;

namespace WebApi
{
    public interface IBuilder
    {
        IList<IProvider> Providers { get; }
        IBuilder Add(IProvider provider);
        IAccessor Build();
    }

    public class Builder : IBuilder
    {
        public IList<IProvider> Providers { get; } = new List<IProvider>();

        public IBuilder Add(IProvider source)
        {
            Providers.Add(source);
            return this;
        }

        public IAccessor Build()
        {
            return new Accessor(Providers);
        }
    }
}
