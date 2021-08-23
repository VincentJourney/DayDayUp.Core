using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public class AeListingOptionsExtension : AbstractListingOptionsExtension
    {
        public AeListingOptionsExtension(Action<ListingOptions> action) : base(action)
        {
        }

        public override void AddServices(IServiceCollection services)
        {
            services.Configure(Configure);
            services.AddTransient<AeEngine>();
            services.AddTransient<AeCreateSkuExcutor>();
            services.AddTransient<AeEnqueueExcutor>();
            services.AddTransient<IProvider, AeProvider>();
        }
    }
}
