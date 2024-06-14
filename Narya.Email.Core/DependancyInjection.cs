using Microsoft.Extensions.DependencyInjection;
using Narya.Email.Core.Interfaces;

namespace Narya.Email.Core;

public static class DependencyInjection
{

    public static IServiceCollection AddEmailProvider(this IServiceCollection services, Func<IServiceCollection, IServiceCollection> register)
    {
        services = register(services);
        services.AddSingleton<IEmailProvider>(provider => EmailProvider.Instance);
        return services;
    }

    public static IServiceProvider AddEmailProvider(this IServiceProvider serviceProvider, string provider, IEmailService providerService)
    {
        var emailProvider = EmailProvider.Instance;
        emailProvider.AddProvider(provider, providerService);
        return serviceProvider;
    }
}
