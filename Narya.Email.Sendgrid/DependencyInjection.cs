﻿using Microsoft.Extensions.DependencyInjection;
using Narya.Email.Core;
using Narya.Email.Core.Interfaces;
using Narya.Email.Sendgrid.Services;

namespace Narya.Email.Sendgrid;

public static class DependencyInjection
{
    public static IServiceCollection AddSendGridProvider(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<EmailService>();
        var serviceProvider = services.BuildServiceProvider();
        serviceProvider.AddEmailProvider("SendGrid", serviceProvider.GetRequiredService<EmailService>());
        return services;
    }
}