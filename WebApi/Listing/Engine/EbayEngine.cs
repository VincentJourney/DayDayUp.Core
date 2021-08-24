using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public class EbayEngine : AbstactEngine
    {
        protected override Platform Platform { get; } = Platform.Ebay;
        public EbayEngine(IAccessor accessor, IOptionsMonitor<ListingOptions> options) : base(accessor, options)
        {
        }
    }
}
