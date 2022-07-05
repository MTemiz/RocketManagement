using System.Text.Json.Serialization;

namespace RocketManagement.Application.Features.Rocket.Dtos
{
    public class TelemetryDto
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }
    }
}
