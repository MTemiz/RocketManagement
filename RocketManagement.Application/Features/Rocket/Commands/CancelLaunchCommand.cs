using MediatR;
using RocketManagement.Application.Features.Rocket.Dtos;

namespace RocketManagement.Application.Features.Rocket.Commands
{
    public class CancelLaunchCommand : IRequest<GenericResponse<RocketDto>>
    {
        public string Id { get; set; }
    }
}
