using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public class EbayListingOptionsExtension : AbstractListingOptionsExtension
    {
        public EbayListingOptionsExtension(Action<ListingOptions> action) : base(action)
        {
        }

        public override void AddServices(IServiceCollection services)
        {
            services.Configure(Configure);
            services.AddTransient<EbayEngine>();
            services.AddTransient<EbayCreateSkuExcutor>();
            services.AddTransient<EbayEnqueueExcutor>();
            services.AddSingleton<IProvider, EbayProvider>();
        }
    }
}
