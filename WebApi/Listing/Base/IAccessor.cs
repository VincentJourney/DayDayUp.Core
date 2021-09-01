using System;
using System.Collections.Generic;

namespace WebApi
{
    public interface IAccessor
    {
        IEnumerable<IListingExcutor> this[Platform key] { get; }
    }

    public class Accessor : IAccessor
    {
        private readonly IList<IProvider> _providers;

        public Accessor(IList<IProvider> providers)
        {
            _providers = providers;
            foreach (var provider in providers)
            {
                provider.Load();
            }
        }

        public IEnumerable<IListingExcutor> this[Platform key]
        {
            get
            {
                foreach (var provider in _providers)
                {
                    if (provider.TryGet(key, out var result))
                    {
                        return result;
                    }
                }

                throw new NotImplementedException($"[{key}]平台没有实现或无法访问服务");
            }
        }
    }
}
