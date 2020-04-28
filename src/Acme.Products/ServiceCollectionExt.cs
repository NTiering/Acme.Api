using Acme.ChangeHandlers;
using Acme.Muators;
using Acme.Toolkit.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Products
{
    public static class ServiceCollectionExt
    {
        public static void AddProductServices(this IServiceCollection serviceCollection)
        {
            var assembly = typeof(ServiceCollectionExt).Assembly;
            serviceCollection.RegisterTypes<IChangeEventHandler>(assembly, RegisterAs.AsScoped);
        }
    }
}