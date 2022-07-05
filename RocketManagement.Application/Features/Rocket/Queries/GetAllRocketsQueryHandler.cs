using MediatR;
using RocketManagement.Application.Features.Rocket.Dtos;
using RocketManagement.Infrastructure.Interfaces;
using System.Text.Json;

namespace RocketManagement.Application.Features.Rocket.Queries
{
    public class GetAllRocketsQueryHandler : IRequestHandler<GetAllRocketsQuery, GenericResponse<List<RocketDto>>>
    {
        private readonly IRocketService rocketService;

        public GetAllRocketsQueryHandler(IRocketService rocketService)
        {
            this.rocketService = rocketService;
        }

        public async Task<GenericResponse<List<RocketDto>>> Handle(GetAllRocketsQuery request, CancellationToken cancellationToken)
        {
            var serviceResponse = await rocketService.GetAllRocketsAsync();

            GenericResponse<List<RocketDto>> retVal = new();

            retVal.IsSuccess = serviceResponse.IsSuccess;
            retVal.Message = serviceResponse.Message;

            if (serviceResponse.IsSuccess)
            {
                retVal.Model = JsonSerializer.Deserialize<List<RocketDto>>(serviceResponse.Response);
            }

            return retVal;

        }
    }
}
