using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi
{
    public interface IBuilder
    {
        IList<ISource> Sources { get; }

        IBuilder Add(ISource source);
        IRoot Build();
    }

    public class Builder : IBuilder
    {
        public IList<ISource> Sources { get; } = new List<ISource>();
        public IBuilder Add(ISource source)
        {
            Sources.Add(source);
            return this;
        }

        public IRoot Build()
        {
            var providers = new List<IProvider>();
            foreach (var source in Sources)
            {
                var provider = source.Build(this);

                providers.Add(provider);
            }

            return new Root(providers);
        }
    }
}
