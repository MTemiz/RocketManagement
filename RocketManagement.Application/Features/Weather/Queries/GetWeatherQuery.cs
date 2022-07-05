using MediatR;
using RocketManagement.Application.Features.Weather.Dtos;

namespace RocketManagement.Application.Features.Weather.Queries
{
    public class GetWeatherQuery : IRequest<GenericResponse<WeatherDto>>
    {
    }
}
