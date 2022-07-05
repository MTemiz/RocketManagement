using System.Text.Json.Serialization;

namespace RocketManagement.Application.Features.Weather.Dtos
{
    public class WindDto
    {
        [JsonPropertyName("direction")]
        public string Direction { get; set; }

        [JsonPropertyName("angle")]
        public float Angle { get; set; }

        [JsonPropertyName("speed")]
        public float Speed { get; set; }
    }
}
