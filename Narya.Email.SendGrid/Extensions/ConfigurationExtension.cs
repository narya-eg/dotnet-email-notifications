using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;

namespace Narya.Email.SendGrid.Extensions;

public static class ConfigurationExtension
{
    public static Result<SendGridConfig> GetSendGridConfig(this IConfiguration configuration)
    {
        var config = configuration.GetSection("SendGrid").Get<SendGridConfig>();
        if (config == null) return Result<SendGridConfig>.Failure("Missing 'SendGrid' configuration section from the appsettings.");
        return Result<SendGridConfig>.Success(config);
    }
}

public class SendGridConfig : IProviderConfig
{
    [Required] public string? ApiKey { get; set; }
    public SendGridFromConfig? From { get; set; }

    public bool ValidateProperty(object instance, string propertyName, object? value)
    {
        return Validator.TryValidateProperty(value, new ValidationContext(instance) {MemberName = propertyName},
            new List<ValidationResult>());
    }
}

public class SendGridFromConfig
{
    public string? Email { get; set; }
    public string? Name { get; set; }
}