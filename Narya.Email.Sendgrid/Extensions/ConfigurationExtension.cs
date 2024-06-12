using Microsoft.Extensions.Configuration;
using Narya.Email.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Narya.Email.Sendgrid.Extensions;

public static class ConfigurationExtension
{
    public static SendGridConfig GetSendGridConfig(this IConfiguration configuration)
    {
        var config = configuration.GetSection("SendGrid").Get<SendGridConfig>();
        if (config == null) throw new Exception("Missing 'SendGrid' configuration section from the appsettings.");
        return config;
    }
}

public class SendGridConfig : IProviderConfig
{
    [Required]
    public string? ApiKey { get; set; }
    public SendGridFromConfig? From { get; set; }
    public bool ValidateProperty(object instance, string propertyName, object? value) => Validator.TryValidateProperty(value, new ValidationContext(instance) { MemberName = propertyName }, new List<ValidationResult>());
}

public class SendGridFromConfig
{
    public string? Email { get; set; }
    public string? Name { get; set; }
}