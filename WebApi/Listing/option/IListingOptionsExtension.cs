using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public interface IListingOptionsExtension
    {
        void AddServices(IServiceCollection services);
    }

    public abstract class AbstractListingOptionsExtension : IListingOptionsExtension
    {
        protected Action<ListingOptions> Configure { get; }

        public AbstractListingOptionsExtension(Action<ListingOptions> action)
        {
            Configure = action;
        }

        public abstract void AddServices(IServiceCollection services);
    }
}
