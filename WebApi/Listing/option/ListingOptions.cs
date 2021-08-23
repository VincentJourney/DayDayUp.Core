using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi
{
    public class ListingOptions
    {
        internal IList<IListingOptionsExtension> Extensions { get; } = new List<IListingOptionsExtension>();

        public void RegisterExtension(IListingOptionsExtension extension)
        {
            Extensions.Add(extension);
        }
    }
}
