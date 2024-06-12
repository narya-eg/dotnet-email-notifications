using Microsoft.Extensions.DependencyInjection;
using Narya.Email.Core.Enums;
using Narya.Email.Core.Interfaces;
using Narya.Email.Sendgrid.Services;

namespace Narya.Email.Sendgrid;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailUsingSendgrid(this IServiceCollection services)
    {
        services.AddSingleton<EmailService>();

        services.AddSingleton<IEmailProvider>(provider =>
        {
            var emailProvider = new EmailProvider();

            emailProvider.AddProvider(EmailProvidersEnum.SendGrid, provider.GetRequiredService<EmailService>());

            return emailProvider;
        });

        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }
}