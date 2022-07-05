using System.Text.Json.Serialization;

namespace RocketManagement.Application.Features.Rocket.Dtos
{
    public class TimestampsDto
    {
        [JsonPropertyName("launched")]
        public DateTime? Launched { get; set; }

        [JsonPropertyName("deployed")]
        public DateTime? Deployed { get; set; }

        [JsonPropertyName("failed")]
        public DateTime? Failed { get; set; }

        [JsonPropertyName("cancelled")]
        public DateTime? Cancelled { get; set; }
    }
}
