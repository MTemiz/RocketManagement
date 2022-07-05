using System.Text.Json.Serialization;

namespace RocketManagement.Application.Features.Rocket.Dtos
{
    public class RocketDto
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("model")]
        public string RocketModel { get; set; }

        [JsonPropertyName("mass")]
        public float Mass { get; set; }

        [JsonPropertyName("payload")]
        public PayloadDto Payload { get; set; }

        [JsonPropertyName("telemetry")]
        public TelemetryDto Telemetry { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("timestamps")]
        public TimestampsDto Timestamps { get; set; }

        [JsonPropertyName("altitude")]
        public float Altitude { get; set; }

        [JsonPropertyName("speed")]
        public float Speed { get; set; }

        [JsonPropertyName("acceleration")]
        public float Acceleration { get; set; }

        [JsonPropertyName("thrust")]
        public float Thrust { get; set; }

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }
    }
}
