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
    public abstract class Provider : IProvider
    {
        protected IDictionary<string, IEnumerable<IListingExcutor>> Data = new Dictionary<string, IEnumerable<IListingExcutor>>();
        public Provider()
        {
        }

        public bool TryGet(string key, out IEnumerable<IListingExcutor> value)
        {
            return Data.TryGetValue(key, out value);
        }

        public void Set(string key, IEnumerable<IListingExcutor> value)
        {
            Data.Add(key, value);
        }
        public virtual void Load()
        {

        }
    }

    public class AeProvider : Provider
    {
        private readonly AeSource _aeSource;
        private readonly string _platform = "Ae";
        public AeProvider(AeSource source)
        {
            _aeSource = source;
        }

        public override void Load()
        {
            Data.Add(_platform, _aeSource.GetListingExcutors());
        }
    }

    public class EbayProvider : Provider
    {
        private readonly EbaySource _ebaySource;
        private readonly string _platform = "Ebay";
        public EbayProvider(EbaySource source)
        {
            _ebaySource = source;
        }

        public override void Load()
        {
            Data.Add(_platform, _ebaySource.GetListingExcutors());
        }
    }
}
