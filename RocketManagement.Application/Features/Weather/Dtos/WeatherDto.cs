using System.Text.Json.Serialization;

namespace RocketManagement.Application.Features.Weather.Dtos
{
    public class WeatherDto
    {
        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public float Humidity { get; set; }

        [JsonPropertyName("pressure")]
        public float Pressure { get; set; }

        [JsonPropertyName("precipitation")]
        public PrecipitationDto Precipitation { get; set; }

        [JsonPropertyName("time")]
        public DateTime? Time { get; set; }

        [JsonPropertyName("wind")]
        public WindDto Wind { get; set; }
    }
}
