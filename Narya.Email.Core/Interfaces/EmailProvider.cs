using Narya.Email.Core.Enums;

namespace Narya.Email.Core.Interfaces;

public class EmailProvider : IEmailProvider
{
    private readonly IDictionary<EmailProvidersEnum, IEmailService> _providers;

    public EmailProvider()
    {
        _providers = new Dictionary<EmailProvidersEnum, IEmailService>();
    }

    public IEmailService GetProvider(EmailProvidersEnum provider)
    {
        if (_providers.TryGetValue(provider, out var emailService))
        {
            return emailService;
        }
        throw new ArgumentException($"Bus with name {provider} not found.");
    }

    public void AddProvider(EmailProvidersEnum provider, IEmailService emailService)
    {
        if (!_providers.ContainsKey(provider))
        {
            _providers.Add(provider, emailService);
        }
    }
}
