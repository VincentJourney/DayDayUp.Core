using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddListingService(this IServiceCollection services, Action<ListingOptions> action)
        {
            return services
                  .AddEngineService(action)
                  .AddEngineFactory();
        }

        public static IServiceCollection AddEngineService(this IServiceCollection services, Action<ListingOptions> action)
        {
            var op = new ListingOptions();
            action(op);
            foreach (var item in op.Extensions)
            {
                item.AddServices(services);
            }

            services.AddTransient<IBuilder, Builder>();
            services.AddTransient<IAccessor>(x =>
            {
                var builder = x.GetRequiredService<IBuilder>();
                foreach (var item in x.GetServices<IProvider>())
                {
                    builder.Add(item);
                }
                return builder.Build();
            });

            return services;
        }

        public static IServiceCollection AddEngineFactory(this IServiceCollection services)
        {
            services.AddSingleton<EngineFactory>();
            return services;
        }
    }
}
