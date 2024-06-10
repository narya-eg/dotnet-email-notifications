namespace Narya.Email.Core.Extensions;

internal static class HtmlExtension
{
    public static bool IsHtml(this string body)
    {
        if (string.IsNullOrWhiteSpace(body)) return false;
        return body.ToLower().Contains("<html") && body.ToLower().Contains("</html>");
    }
}