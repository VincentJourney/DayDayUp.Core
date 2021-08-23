using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace WebApi
{
    public abstract class AbstactEngine : IListingExcutor
    {
        private readonly IEnumerable<IListingExcutor> _listingExcutors;

        protected abstract string Platform { get; }

        public AbstactEngine(IAccessor root)
        {
            _listingExcutors = root[Platform];
        }

        public void Excute(string name)
        {
            foreach (var item in _listingExcutors)
            {
                item.Excute(name);
            }
        }
    }
}
