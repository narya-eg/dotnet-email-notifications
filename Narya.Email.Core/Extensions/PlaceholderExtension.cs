using Narya.Email.Core.Models;

namespace Narya.Email.Core.Extensions;

internal static class PlaceholderExtension
{
    public static string ReplacePlaceholders(this string text, List<EmailPlaceholder> placeholders)
    {
        foreach (var item in placeholders)
        {
            var oldValue = item.Placeholder.Trim();
            text = text.Replace(oldValue, item.Value);
        }

        return text;
    }
}