using Microsoft.Extensions.Options;
using RocketManagement.Infrastructure.Configurations;

namespace RocketManagement.Infrastructure.HttpClients
{
    public class ServiceHttpClient : IServiceHttpClient
    {
        public HttpClient Client { get; }

        public ServiceHttpClient(HttpClient client, IOptions<HttpClientSettings> options)
        {
            Client = client;
            Client.BaseAddress = new Uri(options.Value.BaseAddress);
            Client.Timeout = new TimeSpan(0, 0, options.Value.TimeoutSeconds);
            Client.DefaultRequestHeaders.Add("x-api-key", options.Value.ApiKeyValue);
        }
    }
}
