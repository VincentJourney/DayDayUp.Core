using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi
{
    public interface IBuilder
    {
        IList<IProvider> Sources { get; }
        IBuilder Add(IProvider provider);
        IAccessor Build();
    }

    public class Builder : IBuilder
    {
        public IList<IProvider> Sources { get; } = new List<IProvider>();

        public IBuilder Add(IProvider source)
        {
            Sources.Add(source);
            return this;
        }

        public IAccessor Build()
        {
            return new Accessor(Sources);
        }
    }
}
