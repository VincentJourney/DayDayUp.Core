using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public class AeEngine : AbstactEngine
    {
        protected override string Platform { get; } = "Ae";
        public AeEngine(IAccessor root) : base(root)
        {
        }
    }
}
