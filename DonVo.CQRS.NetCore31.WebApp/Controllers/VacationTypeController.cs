using DonVo.CQRS.Standard21.Application.Requests.VacationType;
using DonVo.CQRS.Standard21.Application.ViewModels.VacationType;
using DonVo.CQRS.NetCore31.WebApp.Infrastructure;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace DonVo.CQRS.NetCore31.WebApp.Controllers
{
    [Authorize(Roles = "CompanyManagement")]
	public class VacationTypeController : Controller
	{
		private readonly IMediator Mediator;

		public VacationTypeController(IMediator mediator)
		{
			Mediator = mediator;
		}

		[HttpGet, LoadModelState]
		public async Task<IActionResult> Index(int id)
		{
			var vacationtypes = await Mediator.Send(new GetVacationTypesQuery { SelectedId = id });
			return View(vacationtypes);
		}

		[HttpPost, ValidateAntiForgeryToken, SaveModelState]
		public async Task<IActionResult> Insert([Bind("Name", "DefaultLeaveDays", "IsPassing", "PoolId")] VacationTypeViewModel viewmodel)
		{
			try
			{
				var response = await Mediator.Send(new InsertVacationTypeCommand { Name = viewmodel.Name, DefaultLeaveDays = viewmodel.DefaultLeaveDays, IsPassing = viewmodel.IsPassing, PoolId = viewmodel.PoolId });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Error", ex.Message);
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpPost, ValidateAntiForgeryToken, SaveModelState]
		public async Task<IActionResult> Update(VacationTypeViewModel viewmodel)
		{
			try
			{
				var response = await Mediator.Send(new UpdateVacationTypeCommand { Id = viewmodel.Id, Name = viewmodel.Name, DefaultLeaveDays = viewmodel.DefaultLeaveDays, IsPassing = viewmodel.IsPassing, PoolId = viewmodel.PoolId, Version = viewmodel.RowVersion });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Error", ex.Message);
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpPost, ValidateAntiForgeryToken, SaveModelState]
		public async Task<IActionResult> Delete(VacationTypeViewModel viewmodel)
		{
			try
			{
				var response = await Mediator.Send(new DeleteVacationTypeCommand { Id = viewmodel.Id });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Error", ex.Message);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}