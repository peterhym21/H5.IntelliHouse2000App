using System.Reflection;
using System.Runtime.CompilerServices;

namespace IntelliHouse2000App.Helpers;

public static class DependencyInjectionScanner
{
    public static void RegisterDependencies(this MauiAppBuilder builder)
    {
        builder.Services.AddByNamespace("IntelliHouse2000App.Repository");
        builder.Services.AddByNamespace("IntelliHouse2000App.Services");
        builder.Services.AddByNamespace("IntelliHouse2000App.ViewModels");
        builder.Services.AddByNamespace("IntelliHouse2000App.Views");
    }
    
    /// <summary>
    /// Recursively adds all classes in namespace. Default lifetime is transient. Single class can be overwritten with [LifeTime(ServiceLifetime.Singleton)]  
    /// </summary>
    /// <param name="namespace">Namespace to (recursively) search for classes in</param>
    /// <returns>Configured service collection</returns>
    public static IServiceCollection AddByNamespace(this IServiceCollection services, string @namespace, ServiceLifetime defaultLifetime = ServiceLifetime.Transient)
    {
        List<ServiceDescriptor> serviceInformation = FindTypes(@namespace, defaultLifetime);

        foreach (ServiceDescriptor service in serviceInformation)
        {
            services.Add(service);
        }

        return services;
    }

    #region Internal logic
    private static List<ServiceDescriptor> FindTypes(string @namespace, ServiceLifetime defaultLifetime)
    {
        List<Type> serviceTypes = GetValidTypesInNamespace(@namespace);

        List<ServiceDescriptor> services = serviceTypes.Select(service =>
        {
            Type implementationType = service.IsInterface
                                      ? serviceTypes.FirstOrDefault(t => $"I{t.Name}" == service.Name)
                                      : service;

            ServiceLifetime lifetime = defaultLifetime;

            if (implementationType.HasAttribute<LifeTimeAttribute>())
            {
                lifetime = implementationType.GetCustomAttribute<LifeTimeAttribute>()!.Lifetime;
            }

            return new ServiceDescriptor(service, implementationType, lifetime);
        }).ToList();

        services = Validate(services);

        return services;
    }
    private static bool HasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
    {
        return type.GetCustomAttribute<TAttribute>() != null;
    }
    private static List<ServiceDescriptor> Validate(List<ServiceDescriptor> services)
    {
        foreach (ServiceDescriptor service in services)
        {
            if (service.ImplementationType == null) throw new ArgumentNullException($"The interface {service.ServiceType.Name} does not have an implementation");
        }

        return services.Where(si => !si.ImplementationType.HasAttribute<IgnoreServiceAttribute>())
                       .ToList();
    }
    private static List<Type> GetValidTypesInNamespace(string @namespace)
    {
        return GetClassesAndInterfacesByNamespace(@namespace)
               .Where(t => !t.HasAttribute<CompilerGeneratedAttribute>())
               .ToList();
    }
    private static List<Type> GetClassesAndInterfacesByNamespace(string @namespace)
    {
        return Assembly.GetAssembly(typeof(DependencyInjectionScanner))!
                       .GetTypes()
                       .Where(t => t != null && t.Namespace != null)
                       .Where(t => t.Namespace!.StartsWith(@namespace) && (t.IsInterface || t.IsClass))
                       .ToList();
    }
    #endregion
}
public class LifeTimeAttribute : Attribute
{
    internal ServiceLifetime Lifetime { get; set; }
    internal LifeTimeAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}
public class IgnoreServiceAttribute : Attribute { }