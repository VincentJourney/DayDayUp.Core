using System;
using System.Collections.Generic;
using System.Text;
using WebApi;

namespace WebApi
{
    public class EbayProvider : BaseProvider
    {
        public EbayProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        public override void Load()
        {
            Data.Add(Platform.Ebay, GetListingExcutors());
        }

        public IEnumerable<Type> GetListingExcutors()
        {
            return new List<Type> {
               typeof( EbayCreateSkuExcutor),
               typeof( EbayEnqueueExcutor)
            };
        }
    }
}
