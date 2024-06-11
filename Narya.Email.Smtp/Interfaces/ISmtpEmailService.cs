
using Narya.Email.Core.Interfaces;
using Narya.Email.Smtp.Extensions;

namespace Narya.Email.Smtp.Interfaces;

public interface ISmtpEmailService<T> : IEmailService where T : class
{
}