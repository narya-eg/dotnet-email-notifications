namespace Narya.Email.Core.Interfaces;

public class EmailProvider : IEmailProvider
{
    private readonly IDictionary<string, IEmailService> _providers;
    private static readonly EmailProvider _instance = new EmailProvider();

    private EmailProvider()
    {
        _providers = new Dictionary<string, IEmailService>();
    }

    // Public static property to provide access to the single instance
    public static EmailProvider Instance
    {
        get { return _instance; }
    }

    public IEmailService GetProvider(string provider)
    {
        if (_providers.TryGetValue(provider, out var emailService))
        {
            return emailService;
        }
        throw new ArgumentException($"Bus with name {provider} not found.");
    }

    public void AddProvider(string provider, IEmailService emailService)
    {
        if (!_providers.ContainsKey(provider))
        {
            _providers.Add(provider, emailService);
        }
    }
}
