using MediatR;
using Microsoft.AspNetCore.Mvc;
using RocketManagement.Application.Features;
using RocketManagement.Application.Features.Weather.Dtos;
using RocketManagement.Application.Features.Weather.Queries;

namespace RocketManagement.UI.ViewComponents
{
    public class WeatherViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public WeatherViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await mediator.Send(new GetWeatherQuery());

            if (result.IsSuccess)
            {
                return await Task.FromResult((IViewComponentResult)View(result.Model));
            }
            else
            {
                SetErrorMessage(result);

                return await Task.FromResult((IViewComponentResult)View(new WeatherDto()));
            }
        }

        private void SetErrorMessage(BaseResponse response)
        {
            TempData["ErrorMessage"] = response.Message;
        }
    }
}
