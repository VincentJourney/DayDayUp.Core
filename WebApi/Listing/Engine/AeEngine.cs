using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public class AeEngine : AbstactEngine
    {
        protected override Platform Platform { get; } = Platform.Ae;
        public AeEngine(IAccessor  accessor, IOptionsMonitor<ListingOptions> options) : base(accessor, options)
        {
        }
    }
}
