using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public abstract class AbstactEngine : IListingExcutor
    {
        protected IRoot Root { get; }

        protected IOptions<ListingOptions> ListingOptions { get; }

        public AbstactEngine(IRoot root, IOptions<ListingOptions> listingOptions)
        {
            Root = root;
            ListingOptions = listingOptions;
        }

        public void Excute(string name)
        {
            var excutors = Root[ListingOptions.Value.Platform];
            foreach (var item in excutors)
            {
                item.Excute(name);
            }
        }
    }
}
