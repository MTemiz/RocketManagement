using MediatR;
using RocketManagement.Application.Features.Rocket.Dtos;
using RocketManagement.Infrastructure.Interfaces;
using System.Text.Json;

namespace RocketManagement.Application.Features.Rocket.Commands
{
    public class LaunchRocketCommandHandler : IRequestHandler<LaunchRocketCommand, GenericResponse<RocketDto>>
    {
        private readonly IRocketService rocketService;

        public LaunchRocketCommandHandler(IRocketService rocketService)
        {
            this.rocketService = rocketService;
        }

        public async Task<GenericResponse<RocketDto>> Handle(LaunchRocketCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = await rocketService.LaunchAsync(request.Id);

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
