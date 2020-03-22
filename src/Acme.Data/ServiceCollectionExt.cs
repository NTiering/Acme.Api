using Acme.ChangeHandlers;
using Acme.Data.Context;
using Acme.Muators;
using Acme.Toolkit.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Data
{
    public static class ServiceCollectionExt
    {
        public static void AddDataServices(this IServiceCollection serviceCollection)
        {
            var assembly = typeof(ServiceCollectionExt).Assembly;

            serviceCollection.RegisterTypes<IChangeEventHandler>(assembly, RegisterAs.AsScoped);
            serviceCollection.RegisterTypes<ICrudDataTools>(assembly, RegisterAs.AsScoped);
            serviceCollection.RegisterTypes<IDataMutatorHandler>(assembly, RegisterAs.AsScoped);
            serviceCollection.RegisterTypes<IDataContext>(assembly, RegisterAs.AsScoped);
            serviceCollection.RegisterTypes<ISearchContext>(assembly, RegisterAs.AsScoped);
        }
    }
}