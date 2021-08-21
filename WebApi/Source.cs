using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi
{
    public interface ISource
    {
        IProvider Build(IBuilder builder);

        IEnumerable<IListingExcutor> GetListingExcutors();
    }

    public class AeSource : ISource
    {
        public IProvider Build(IBuilder builder)
        {
            return new AeProvider(this);
        }

        public IEnumerable<IListingExcutor> GetListingExcutors()
        {
            return new List<IListingExcutor> {
                new AeCreateSkuExcutor(),
                new AeEnqueueExcutor()
            };
        }
    }

    public class EbaySource : ISource
    {
        public IProvider Build(IBuilder builder)
        {
            return new EbayProvider(this);
        }
        public IEnumerable<IListingExcutor> GetListingExcutors()
        {
            return new List<IListingExcutor> {
                new EbayCreateSkuExcutor(),
                new EbayEnqueueExcutor()
            };
        }
    }
}
