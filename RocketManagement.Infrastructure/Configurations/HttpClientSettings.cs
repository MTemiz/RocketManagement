namespace RocketManagement.Infrastructure.Configurations
{
    public class HttpClientSettings
    {
        public int TimeoutSeconds { get; set; }
        public string BaseAddress { get; set; }
        public string ApiKeyValue { get; set; }
    }
}
