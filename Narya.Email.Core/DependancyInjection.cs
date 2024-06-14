using Microsoft.Extensions.DependencyInjection;
using Narya.Email.Core.Interfaces;

namespace Narya.Email.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailProvider(this IServiceCollection services, Func<IServiceProvider, IEmailProvider> config)
    {
        services.AddSingleton<IEmailProvider>(config);
        return services;
    }

    public static IServiceProvider AddEmailProvider(this IServiceProvider serviceProvider, string provider, IEmailService providerService)
    {
        var emailProvider = EmailProvider.Instance;

        // Add providers to the EmailProvider instance
        emailProvider.AddProvider(provider, providerService);

        return serviceProvider;
    }
}
