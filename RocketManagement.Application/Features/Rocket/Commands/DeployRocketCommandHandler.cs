using MediatR;
using RocketManagement.Application.Features.Rocket.Dtos;
using RocketManagement.Infrastructure.Interfaces;
using System.Text.Json;

namespace RocketManagement.Application.Features.Rocket.Commands
{
    public class DeployRocketCommandHandler : IRequestHandler<DeployRocketCommand, GenericResponse<RocketDto>>
    {
        private readonly IRocketService rocketService;

        public DeployRocketCommandHandler(IRocketService rocketService)
        {
            this.rocketService = rocketService;
        }

        public async Task<GenericResponse<RocketDto>> Handle(DeployRocketCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = await rocketService.DeployAsync(request.Id);

            GenericResponse<RocketDto> retVal = new();

            retVal.IsSuccess = serviceResponse.IsSuccess;
            retVal.Message = serviceResponse.Message;

            if (serviceResponse.IsSuccess)
            {
                retVal.Model = JsonSerializer.Deserialize<RocketDto>(serviceResponse.Response);
            }

            return retVal;
        }
    }
}
