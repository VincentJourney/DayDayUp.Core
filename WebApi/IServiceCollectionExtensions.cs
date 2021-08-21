using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public class ListingOptions
    {
        public string Platform { get; set; }

        internal IList<IListingOptionsExtension> Extensions { get; } = new List<IListingOptionsExtension>();

        public void RegisterExtension(IListingOptionsExtension extension)
        {
            Extensions.Add(extension);
        }
    }

    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddEngineService(this IServiceCollection services, Action<ListingOptions> action)
        {
            var op = new ListingOptions();
            action(op);
            foreach (var item in op.Extensions)
            {
                item.AddServices(services);
            }
            return services;
        }

        public static IServiceCollection AddEngineFactory(this IServiceCollection services)
        {
            services.AddSingleton<EngineFactory>();
            return services;
        }
    }

    public class EngineFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public EngineFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Create<T>() where T : AbstactEngine
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }

    public static class OptionsExtensions
    {
        public static ListingOptions UseAeEngine(this ListingOptions option)
        {
            option.RegisterExtension(new AeListingOptionsExtension(s =>
            {
                s.Platform = "Ae";
            }));

            return option;
        }

        public static ListingOptions UseEbayEngine(this ListingOptions option)
        {
            option.RegisterExtension(new AeListingOptionsExtension(s =>
            {
                s.Platform = "Ebay";
            }));

            return option;
        }
    }

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
    public class AeListingOptionsExtension : AbstractListingOptionsExtension
    {
        public AeListingOptionsExtension(Action<ListingOptions> action) : base(action)
        {
        }

        public override void AddServices(IServiceCollection services)
        {
            services.Configure(Configure);
            services.AddTransient<AeEngine>();
            services.AddTransient<IBuilder, Builder>();
            services.AddTransient<IRoot>(x =>
            {
                var root = x.GetRequiredService<IBuilder>()
                   .Add(new AeSource())
                   .Build();

                return root;
            });
        }
    }
}
