using Microsoft.Extensions.DependencyInjection;
using Narya.Email.Core.Interfaces;
using Narya.Email.Sendgrid.Services;
using Narya.Email.Core;

namespace Narya.Email.Sendgrid;

public static class DependencyInjection
{
    //public static IServiceCollection AddSendGrid(this IServiceCollection services)
    //{
    //    services.AddSingleton<EmailService>();

    //    services.AddSingleton<IEmailService, EmailService>();

    //    return services;
    //}

    public static IServiceCollection AddSendGridProvider(this IServiceCollection services, IServiceProvider serviceProvider)
    {
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<EmailService>();
        serviceProvider.AddEmailProvider("SendGrid", serviceProvider.GetRequiredService<EmailService>());
        return services;
    }
}