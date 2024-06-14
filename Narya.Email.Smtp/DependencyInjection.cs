using Microsoft.Extensions.DependencyInjection;
using Narya.Email.Core;
using Narya.Email.Core.Interfaces;
using Narya.Email.Smtp.Services;

namespace Narya.Email.Smtp;

public static class DependencyInjection
{
    //public static IServiceCollection AddSmtp(this IServiceCollection services)
    //{
    //    services.AddSingleton<EmailService>();

    //    services.AddEmailProvider(serviceProvider =>
    //    {
    //        serviceProvider.AddEmailProvider("Smtp", serviceProvider.GetRequiredService<EmailService>());
    //        return EmailProvider.Instance;
    //    });
      
    //    services.AddSingleton<IEmailService, EmailService>();

    //    return services;
    //}

    public static IServiceCollection AddSmtpProvider(this IServiceCollection services)
    {
        services.AddSingleton<EmailService>();
        services.AddSingleton<IEmailService, EmailService>();
        var serviceProvider = services.BuildServiceProvider();
        serviceProvider.AddEmailProvider("Smtp", serviceProvider.GetRequiredService<EmailService>());
        return services;
    }
}