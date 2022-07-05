using MediatR;
using RocketManagement.Application.Features.Rocket.Dtos;

namespace RocketManagement.Application.Features.Rocket.Commands
{
    public class DeployRocketCommand : IRequest<GenericResponse<RocketDto>>
    {
        public string Id { get; set; }
    }
}
