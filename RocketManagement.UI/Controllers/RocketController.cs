using MediatR;
using Microsoft.AspNetCore.Mvc;
using RocketManagement.Application.Features;
using RocketManagement.Application.Features.Rocket.Commands;
using RocketManagement.Application.Features.Rocket.Dtos;
using RocketManagement.Application.Features.Rocket.Queries;

namespace RocketManagement.UI.Controllers
{
    public class RocketController : Controller
    {
        private readonly ILogger<RocketController> _logger;
        private readonly IMediator mediator;

        public RocketController(ILogger<RocketController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await mediator.Send(new GetAllRocketsQuery());

            if (!result.IsSuccess)
            {
                IfNotSuccessSetErrorMessage(result);

                return View(new List<RocketDto>());
            }

            return View(result.Model);
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(string id)
        {
            var result = await mediator.Send(new GetAllRocketsQuery());

            if (!result.IsSuccess)
            {
                IfNotSuccessSetErrorMessage(result);

                return View(new RocketDto());
            }

            var rocket = result.Model.First(c => c.Id == id);

            return View(rocket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("launch")]
        public async Task<IActionResult> Launch([Bind("Id")] string id)
        {
            var result = await mediator.Send(new LaunchRocketCommand() { Id = id });

            SetMessageToTempData(result);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("deploy")]
        public async Task<IActionResult> Deploy([Bind("Id")] string id)
        {
            var result = await mediator.Send(new DeployRocketCommand() { Id = id });

            SetMessageToTempData(result);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("cancellaunch")]
        public async Task<IActionResult> CancelLaunch([Bind("Id")] string id)
        {
            var result = await mediator.Send(new CancelLaunchCommand() { Id = id });

            SetMessageToTempData(result);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        [Route("show-telemetry")]
        public async Task<IActionResult> ShowTelemetry(string id)
        {
            return View();
        }

        private void SetMessageToTempData(BaseResponse response)
        {
            IfSuccessSetInformationMessage(response);
            IfNotSuccessSetErrorMessage(response);
        }

        private void IfNotSuccessSetErrorMessage(BaseResponse response)
        {
            if (!response.IsSuccess)
            {
                TempData["ErrorMessage"] = response.Message;
            }
        }

        private void IfSuccessSetInformationMessage(BaseResponse response)
        {
            if (response.IsSuccess)
            {
                TempData["InformationMessage"] = response.Message;
            }
        }
    }
}