using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi
{
    public interface IAccessor
    {
        IEnumerable<IListingExcutor> this[string key] { get; set; }

    }

    public class Accessor : IAccessor
    {
        public Accessor(IList<IProvider> providers)
        {
            _providers = providers;
            foreach (var provider in providers)
            {
                provider.Load();
            }
        }

        public IEnumerable<IListingExcutor> this[string key]
        {
            get
            {
                for (int i = _providers.Count - 1; i >= 0; i--)
                {
                    IProvider configurationProvider = _providers[i];
                    if (configurationProvider.TryGet(key, out var result))
                    {
                        return result;
                    }
                }
                return null;
            }
            set
            {
                if (!_providers.Any<IProvider>())
                {
                    throw new InvalidOperationException();
                }
                foreach (IProvider configurationProvider in _providers)
                {
                    configurationProvider.Set(key, value);
                }
            }
        }

        private readonly IList<IProvider> _providers;

        public IEnumerable<IProvider> Providers { get => _providers; }
    }
}
