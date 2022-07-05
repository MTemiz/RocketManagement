using MediatR;
using RocketManagement.Application.Features.Rocket.Dtos;

namespace RocketManagement.Application.Features.Rocket.Queries
{
    public class GetAllRocketsQuery : IRequest<GenericResponse<List<RocketDto>>>
    {
    }
}
