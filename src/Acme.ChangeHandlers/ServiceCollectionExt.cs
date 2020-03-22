using Acme.Toolkit.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.ChangeHandlers
{
    public static class ServiceCollectionExt
    {
        public static void AddChangeTrackerServices(this IServiceCollection serviceCollection)
        {
            var assembly = typeof(ServiceCollectionExt).Assembly;
            serviceCollection.RegisterTypes<IChangeTracker>(assembly, RegisterAs.AsScoped);
        }
    }
}