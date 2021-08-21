using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public class AeEngine : AbstactEngine
    {
        public AeEngine(IRoot root, IOptions<ListingOptions> listingOptions) : base(root, listingOptions)
        {
        }
    }

    //public class EbayEngine : AbstactEngine
    //{
    //    public EbayEngine(IRoot root, IOptions<ListingOptions> listingOptions) : base(root, listingOptions)
    //    {
    //    }
    //}
}
