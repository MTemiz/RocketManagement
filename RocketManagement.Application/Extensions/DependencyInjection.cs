using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RocketManagement.Application.Features;
using RocketManagement.Application.Features.Rocket.Commands;
using RocketManagement.Application.Features.Rocket.Dtos;
using RocketManagement.Application.Features.Rocket.Queries;
using RocketManagement.Application.Features.Weather.Dtos;
using RocketManagement.Application.Features.Weather.Queries;
using System.Reflection;

namespace RocketManagement.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IRequestHandler<CancelLaunchCommand, GenericResponse<RocketDto>>), typeof(CancelLaunchCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeployRocketCommand, GenericResponse<RocketDto>>), typeof(DeployRocketCommandHandler));
            services.AddScoped(typeof(IRequestHandler<LaunchRocketCommand, GenericResponse<RocketDto>>), typeof(LaunchRocketCommandHandler));

            services.AddScoped(typeof(IRequestHandler<GetAllRocketsQuery, GenericResponse<List<RocketDto>>>), typeof(GetAllRocketsQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetWeatherQuery, GenericResponse<WeatherDto>>), typeof(GetWeatherQueryHandler));
        }
    }
}
