namespace Narya.Email.Core.Interfaces;

public interface IEmailProvider
{
    IEmailService GetProvider(string provider);
    void AddProvider(string provider, IEmailService busControl);
}
