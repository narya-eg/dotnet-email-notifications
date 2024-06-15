using System.Net;
using System.Runtime.Serialization;
using SendGrid;

namespace Narya.Email.Sendgrid.Exceptions;

[Serializable]
internal class SendGridException : Exception
{
    private SendGridException()
    {
    }

    private SendGridException(string message) : base(message)
    {
    }

    private SendGridException(string message, Exception innerException) : base(message, innerException)
    {
    }

    private SendGridException(string message, int statusCode, string responseData,
        Dictionary<string, IEnumerable<string>> headers) : base(message)
    {
        StatusCode = statusCode;
        ResponseData = responseData;
        Headers = headers;
    }

    private SendGridException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public int StatusCode { get; private set; }
    public string ResponseData { get; private set; } = string.Empty;
    public Dictionary<string, IEnumerable<string>>? Headers { get; private set; }

    public static async Task ThrowExceptionOnError(Response response)
    {
        if (response.StatusCode != HttpStatusCode.OK &&
            response.StatusCode != HttpStatusCode.Accepted &&
            response.StatusCode != HttpStatusCode.Created &&
            response.StatusCode != HttpStatusCode.NoContent)
        {
            var headers = response.Headers.ToDictionary(header => header.Key, header => header.Value);
            var responseData = response.Body == null ? null : await response.Body.ReadAsStringAsync();

            var errorMessage =
                $"The HTTP status code of the response was not expected ({response.StatusCode}-{(int) response.StatusCode})";

            throw new SendGridException(
                errorMessage,
                (int) response.StatusCode,
                responseData,
                headers);
        }
    }
}