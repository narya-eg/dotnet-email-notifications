using Microsoft.Extensions.DependencyInjection;
using Narya.Email.Core.Interfaces;
using Narya.Email.Smtp.Extensions;
using Narya.Email.Smtp.Interfaces;
using Narya.Email.Smtp.Services;

namespace Narya.Email.Smtp;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailUsingSmtp(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, EmailService>();
        return services;
    }
}