using DonVo.CQRS.Standard21.Application.Requests.Holiday;
using DonVo.CQRS.Standard21.Application.ViewModels.Holiday;
using DonVo.CQRS.NetCore31.WebApp.Infrastructure;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace DonVo.CQRS.NetCore31.WebApp.Controllers
{
    [Authorize(Roles = "HolidaysManagement")]
    public class HolidayController : Controller
    {
        private readonly IMediator Mediator;

        public HolidayController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet, LoadModelState]
        public async Task<IActionResult> Index()
        {
            var holidays = await Mediator.Send(new GetHolidaysQuery());
            return View(holidays);
        }

        [HttpPost, ValidateAntiForgeryToken, SaveModelState]
        public async Task<IActionResult> Insert([Bind("Date")] HolidayViewModel viewmodel)
        {
            try
            {
                var response = await Mediator.Send(new InsertHolidayCommand { Date = viewmodel.Date });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken, SaveModelState]
        public async Task<IActionResult> Delete(HolidayViewModel viewmodel)
        {
            try
            {
                var response = await Mediator.Send(new DeleteHolidayCommand { Id = viewmodel.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}