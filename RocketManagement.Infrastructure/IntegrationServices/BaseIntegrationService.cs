using Polly;
using RocketManagement.Infrastructure.IntegrationServices.Responses;

namespace RocketManagement.Infrastructure.IntegrationServices
{
    public abstract class BaseIntegrationService
    {
        private readonly IAsyncPolicy<HttpResponseMessage> retryPolicy;

        protected BaseIntegrationService(IAsyncPolicy<HttpResponseMessage> retryPolicy)
        {
            this.retryPolicy = retryPolicy;
        }

        protected async Task<IntegrationServiceResponse> CallService(Func<Task<HttpResponseMessage>> serviceFunc)
        {
            var responseMessage = await retryPolicy.ExecuteAsync(async () => { return await serviceFunc(); });

            IntegrationServiceResponse retVal = new();

            retVal.IsSuccess = responseMessage.IsSuccessStatusCode;

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();

                retVal.Response = responseContent;

                retVal.Message = "successful";
            }
            else
            {
                retVal.Message = responseMessage.ReasonPhrase;
            }

            return retVal;
        }
    }
}
