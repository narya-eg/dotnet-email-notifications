﻿using Microsoft.Extensions.DependencyInjection;
using Narya.Email.Core.Enums;
using Narya.Email.Core.Interfaces;
using Narya.Email.Smtp.Services;

namespace Narya.Email.Smtp;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailUsingSmtp(this IServiceCollection services)
    {
        services.AddSingleton<EmailService>();

        services.AddSingleton<IEmailProvider>(provider =>
        {
            var emailProvider = new EmailProvider();

            // Add providers to the EmailProvider instance
            emailProvider.AddProvider(EmailProvidersEnum.Smtp, provider.GetRequiredService<EmailService>());

            return emailProvider;
        });

        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }
}