using Narya.Email.Core.Enums;

namespace Narya.Email.Core.Interfaces;

public interface IEmailProvider
{
    IEmailService GetProvider(EmailProvidersEnum provider);
    void AddProvider(EmailProvidersEnum provider, IEmailService busControl);
}
