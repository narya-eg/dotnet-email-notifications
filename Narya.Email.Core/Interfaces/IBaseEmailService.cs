using Narya.Email.Core.Models;

namespace Narya.Email.Core.Interfaces;

public interface IBaseEmailService
{
    /// <summary>
    /// Read the configuration from the appsettings.json and send an email address
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    Task SendEmail(EmailOptions options);
    
    /// <summary>
    /// Send an email using the passed configuration
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    /// <typeparam name="TProviderConfig"></typeparam>
    /// <returns></returns>
    Task SendEmail<TProviderConfig>(EmailOptions options, TProviderConfig configuration);
}