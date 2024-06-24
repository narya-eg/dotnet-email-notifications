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
        if (config.ValidateObject(config, out List<ValidationResult> results)) return Result<SendGridConfig>.Failure(string.Join(",", results.Select(x => x.ErrorMessage)));
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

    public bool ValidateObject(object instance, out List<ValidationResult> validationResults)
    {
        validationResults = new();
        return Validator.TryValidateObject(instance, new ValidationContext(instance),
            validationResults);
    }
}

public class SendGridFromConfig
{
    public string? Email { get; set; }
    public string? Name { get; set; }
}