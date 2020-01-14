using DonVo.CQRS.Standard21.Application.Requests.Position;
using DonVo.CQRS.Standard21.Application.ViewModels.Position;
using DonVo.CQRS.NetCore31.WebApp.Infrastructure;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace DonVo.CQRS.NetCore31.WebApp.Controllers
{
    [Authorize(Roles = "CompanyManagement")]
    public class PositionController : Controller
    {
        private readonly IMediator Mediator;

        public PositionController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet, LoadModelState]
        public async Task<IActionResult> Index(int id)
        {
            var positions = await Mediator.Send(new GetPositionsQuery { SelectedId = id });
            return View(positions);
        }

        [HttpPost, ValidateAntiForgeryToken, SaveModelState]
        public async Task<IActionResult> Insert([Bind("Name", "Description", "Grade")] PositionViewModel viewmodel)
        {
            try
            {
                var response = await Mediator.Send(new InsertPositionCommand { Name = viewmodel.Name, Description = viewmodel.Description, Grade = viewmodel.Grade });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken, SaveModelState]
        public async Task<IActionResult> Update(PositionViewModel viewmodel)
        {
            try
            {
                var response = await Mediator.Send(new UpdatePositionCommand { Id = viewmodel.Id, Name = viewmodel.Name, Description = viewmodel.Description, Grade = viewmodel.Grade, Version = viewmodel.RowVersion });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken, SaveModelState]
        public async Task<IActionResult> Delete(PositionViewModel viewmodel)
        {
            try
            {
                var response = await Mediator.Send(new DeletePositionCommand { Id = viewmodel.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}