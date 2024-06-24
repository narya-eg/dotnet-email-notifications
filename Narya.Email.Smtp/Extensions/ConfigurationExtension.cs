using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Narya.Email.Core.Interfaces;
using Narya.Email.Core.Models;

namespace Narya.Email.Smtp.Extensions;

public static class ConfigurationExtension
{
    public static Result<SmtpConfig> GetSmtpConfig(this IConfiguration configuration)
    {
        var config = configuration.GetSection("Smtp").Get<SmtpConfig>();
        if (config == null) return Result<SmtpConfig>.Failure("Missing 'Smtp' configuration section from the appsettings.");
        if (config.ValidateObject(config, out List<ValidationResult> results) is false) return Result<SmtpConfig>.Failure(string.Join(",", results.Select(x => x.ErrorMessage)));
        return Result<SmtpConfig>.Success(config);
    }
}

public class SmtpConfig : IProviderConfig
{
    [Required] public string? Server { get; set; }
    [Required] [StringLength(100)] public string? Username { get; set; }
    [Required] [StringLength(100)] public string? Password { get; set; }
    [Required] public int Port { get; set; }
    [Required] public bool EnableSsl { get; set; }
    public bool IgnoreCertificateErrors { get; set; }
    public SmtpFromConfig? From { get; set; }

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

public class SmtpFromConfig
{
    public string? Email { get; set; }
    public string? Name { get; set; }
}