using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public abstract class AbstactEngine 
    {
        protected readonly IEnumerable<IListingExcutor> _listingExcutors;

        protected abstract Platform Platform { get; }

        protected ListingOptions ListingOptions { get; }

        public AbstactEngine(IAccessor accessor, IOptionsMonitor<ListingOptions> options)
        {
            _listingExcutors = accessor[Platform];
            ListingOptions = options.CurrentValue;
        }

        public void Excute(string name)
        {
            foreach (var item in _listingExcutors)
            {
                item.Excute(name);
                Console.WriteLine($"{name} --{item.GetType().FullName} hashcode-- {item.GetHashCode()}");
            }
        }
    }
}
