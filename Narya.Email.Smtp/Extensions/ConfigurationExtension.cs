using Microsoft.Extensions.Configuration;
using Narya.Email.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Narya.Email.Smtp.Extensions;

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

public class SmtpConfig: IProviderConfig
{
    [Required]
    public string? Server { get; set; }
    [Required]
    [StringLength(100)]
    public string? Username { get; set; }
    [Required]
    [StringLength(100)]
    public string? Password { get; set; }
    [Required]
    public int Port { get; set; }
    [Required]
    public bool EnableSsl { get; set; }
    public bool IgnoreCertificateErrors { get; set; }
    public SmtpFromConfig? From { get; set; }

    public bool ValidateProperty(object instance, string propertyName, object? value) => Validator.TryValidateProperty(value, new ValidationContext(instance) { MemberName = propertyName }, new List<ValidationResult>());
}

public class SmtpFromConfig
{
    public string? Email { get; set; }
    public string? Name { get; set; }
}