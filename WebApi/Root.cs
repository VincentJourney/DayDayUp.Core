using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi
{
    public interface IRoot
    {
        IEnumerable<IListingExcutor> this[string key] { get; set; }

        T GetSection<T>(string key);
    }
    public class Root : IRoot
    {
        public IEnumerable<IListingExcutor> this[string key]
        {
            get
            {
                for (int i = this._providers.Count - 1; i >= 0; i--)
                {
                    IProvider configurationProvider = this._providers[i];
                    if (configurationProvider.TryGet(key, out var result))
                    {
                        return result;
                    }
                }
                return null;
            }
            set
            {
                if (!this._providers.Any<IProvider>())
                {
                    throw new InvalidOperationException();
                }
                foreach (IProvider configurationProvider in this._providers)
                {
                    configurationProvider.Set(key, value);
                }
            }
        }

        private readonly IList<IProvider> _providers;

        public IEnumerable<IProvider> Providers
        {
            get
            {
                return this._providers;
            }
        }
        public Root(IList<IProvider> providers)
        {
            _providers = providers;
            foreach (var provider in providers)
            {
                provider.Load();
            }
        }

        public T GetSection<T>(string key)
        {

            return default;
        }
    }
}
