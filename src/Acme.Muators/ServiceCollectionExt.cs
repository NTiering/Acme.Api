using Acme.Toolkit.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Muators
{
    public static class ServiceCollectionExt
    {
        public static void AddDataMutatorsServices(this IServiceCollection serviceCollection)
        {
            var assembly = typeof(ServiceCollectionExt).Assembly;
            serviceCollection.RegisterTypes<IDataMutator>(assembly, RegisterAs.AsScoped);
        }
    }
}