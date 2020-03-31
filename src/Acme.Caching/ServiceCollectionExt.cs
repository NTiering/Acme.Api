using Acme.Toolkit.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Caching
{
    public static class ServiceCollectionExt
    {
        public static void AddCaching(this IServiceCollection serviceCollection)
        {
            var assembly = typeof(ServiceCollectionExt).Assembly;
            serviceCollection.RegisterTypes<ICacheProvider>(assembly, RegisterAs.AsSingleton);
        }
        
    }
}
