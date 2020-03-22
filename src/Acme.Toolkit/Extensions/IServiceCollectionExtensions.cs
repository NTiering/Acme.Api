using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Acme.Toolkit.Extensions
{
    public enum RegisterAs
    {
        AsTransient, AsSingleton, AsScoped
    }

    public static class IServiceCollectionExtensions
    {
        public static int RegisterTypes<T>(this IServiceCollection services, Assembly assembly, RegisterAs registerAs, bool throwErrorOnNonFound = true)
        {
            var types = assembly.GetTypesThatImplement<T>().ToList();

            if (throwErrorOnNonFound && types.Any() == false)
            {
                throw new InvalidOperationException($"No implementations found for {typeof(T).Name}");
            }

            var serviceType = typeof(T);

            if (registerAs == RegisterAs.AsScoped) types.ForEach(x => services.AddScoped(serviceType, x));
            if (registerAs == RegisterAs.AsSingleton) types.ForEach(x => services.AddSingleton(serviceType, x));
            if (registerAs == RegisterAs.AsTransient) types.ForEach(x => services.AddTransient(serviceType, x));

            return types.Count();
        }

        public static void RegisterTypes<T>(this IServiceCollection services, Assembly[] assembly, RegisterAs registerAs)
        {
            assembly.ToList()
                .ForEach(service => services.RegisterTypes<T>(service, registerAs));
        }
    }
}