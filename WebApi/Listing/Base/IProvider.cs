using System;
using System.Collections.Generic;
using System.Text;
using WebApi;

namespace WebApi
{
    public interface IProvider
    {
        void Load();
        void Set(string key, IEnumerable<IListingExcutor> value);
        bool TryGet(string key, out IEnumerable<IListingExcutor> value);
    }

    public abstract class BaseProvider : IProvider
    {
        protected IDictionary<string, IEnumerable<IListingExcutor>> Data = new Dictionary<string, IEnumerable<IListingExcutor>>();

        public bool TryGet(string key, out IEnumerable<IListingExcutor> value)
        {
            return Data.TryGetValue(key, out value);
        }

        public void Set(string key, IEnumerable<IListingExcutor> value)
        {
            Data.Add(key, value);
        }

        public virtual void Load() { }
    }
}
