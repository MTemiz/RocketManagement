using System.Text.Json.Serialization;

namespace RocketManagement.Application.Features.Rocket.Dtos
{
    public class PayloadDto
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }
    }
}
