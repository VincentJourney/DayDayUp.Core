using System;
using System.Collections.Generic;

namespace WebApi
{
    public interface IProvider
    {
        void Load();

        bool TryGet(Platform key, out IEnumerable<IListingExcutor> value);
    }

    public abstract class BaseProvider : IProvider
    {
        protected IServiceProvider ServiceProvider { get; }

        protected IDictionary<Platform, IEnumerable<Type>> Data { get; } = new Dictionary<Platform, IEnumerable<Type>>();

        public BaseProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public bool TryGet(Platform key, out IEnumerable<IListingExcutor> value)
        {
            IList<IListingExcutor> excutors = new List<IListingExcutor>();
            value = excutors;
            var result = Data.TryGetValue(key, out var types);
            if (!result) return result;

            foreach (var item in types)
            {
                excutors.Add((IListingExcutor)ServiceProvider.GetService(item));
            }

            return true;
        }

        public virtual void Load() { }
    }
}
