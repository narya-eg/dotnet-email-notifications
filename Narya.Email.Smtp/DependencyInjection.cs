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

    public static IServiceCollection AddSmtpProvider(this IServiceCollection services, IServiceProvider serviceProvider)
    {
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<EmailService>();
        serviceProvider.AddEmailProvider("Smtp", serviceProvider.GetRequiredService<EmailService>());
        return services;
    }
}