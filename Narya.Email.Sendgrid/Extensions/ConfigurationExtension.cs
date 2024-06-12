using Microsoft.Extensions.Configuration;
using Narya.Email.Core.Interfaces;

namespace Narya.Email.Sendgrid.Extensions;

public static class ConfigurationExtension
{
    public static SmtpConfig GetSmtpConfig(this IConfiguration configuration)
    {
        var config = configuration.GetSection("Smtp").Get<SmtpConfig>();
        if (config == null)
            throw new Exception("Missing 'Smtp' configuration section from the appsettings.");
        return config;
    }
}

public class SmtpConfig : IProviderConfig
{
    public string? Server { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public bool IgnoreCertificateErrors { get; set; }
    public SmtpFromConfig? From { get; set; }

    public bool RequiredProperty(string name)
    {
        return new string[] { "Server", "Username", "Password", "Port", "EnableSsl" }.Contains(name);
    }
}

public class SmtpFromConfig
{
    public string? Email { get; set; }
    public string? Name { get; set; }
}