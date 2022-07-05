using System.Text.Json.Serialization;

namespace RocketManagement.Application.Features.Weather.Dtos
{
    public class PrecipitationDto
    {
        [JsonPropertyName("probability")]
        public float Probability { get; set; }

        [JsonPropertyName("rain")]
        public bool Rain { get; set; }

        [JsonPropertyName("snow")]
        public bool Snow { get; set; }

        [JsonPropertyName("sleet")]
        public bool Sleet { get; set; }

        [JsonPropertyName("hail")]
        public bool Hail { get; set; }
    }
}
