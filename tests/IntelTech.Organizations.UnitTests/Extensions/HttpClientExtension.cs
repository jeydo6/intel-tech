using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace IntelTech.Organizations.UnitTests.Extensions;

internal static class HttpClientExtension
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

    public static Task<HttpResponseMessage> PostAsync<TContent>(this HttpClient httpClient, string requestUri, TContent content,
        CancellationToken cancellationToken = default)
    {
        var jsonContent = JsonSerializer.Serialize(content, _jsonSerializerOptions);
        var stringContent = new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        return httpClient.PostAsync(requestUri, stringContent, cancellationToken);
    }
}
