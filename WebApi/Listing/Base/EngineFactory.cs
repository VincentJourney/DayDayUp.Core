using System;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
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
}
