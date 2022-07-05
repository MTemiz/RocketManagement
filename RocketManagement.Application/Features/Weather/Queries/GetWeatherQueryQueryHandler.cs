using MediatR;
using RocketManagement.Application.Features.Weather.Dtos;
using RocketManagement.Infrastructure.Interfaces;
using System.Text.Json;

namespace RocketManagement.Application.Features.Weather.Queries
{
    public class GetWeatherQueryHandler : IRequestHandler<GetWeatherQuery, GenericResponse<WeatherDto>>
    {
        private readonly IWeatherService weatherService;

        public GetWeatherQueryHandler(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        public async Task<GenericResponse<WeatherDto>> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
        {
            var serviceResponse = await weatherService.GetWeatherAsync();

            GenericResponse<WeatherDto> retVal = new();

            retVal.IsSuccess = serviceResponse.IsSuccess;
            retVal.Message = serviceResponse.Message;

            if (serviceResponse.IsSuccess)
            {
                retVal.Model = JsonSerializer.Deserialize<WeatherDto>(serviceResponse.Response);
            }

            return retVal;
        }
    }
}
