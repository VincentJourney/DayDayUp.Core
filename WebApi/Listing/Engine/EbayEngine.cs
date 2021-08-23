using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public class EbayEngine : AbstactEngine
    {
        protected override string Platform { get; } = "Ebay";
        public EbayEngine(IAccessor root) : base(root)
        {
        }
    }
}
