using System.Net.Mail;
using Microsoft.AspNetCore.Http;

namespace Narya.Email.Sendgrid.Helpers;

public static class AttachmentExtension
{
    internal static Attachment ToAttachment(this IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        var fileBytes = ms.ToArray();
        return new Attachment(new MemoryStream(fileBytes), file.FileName);
    }
}