using DonVo.CQRS.Standard21.Application.Requests.Department;
using DonVo.CQRS.Standard21.Application.ViewModels.Department;
using DonVo.CQRS.NetCore31.WebApp.Infrastructure;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace DonVo.CQRS.NetCore31.WebApp.Controllers
{
    [Authorize(Roles = "CompanyManagement")]
	public class DepartmentController : Controller
	{
		private readonly IMediator Mediator;

		public DepartmentController(IMediator mediator)
		{
			Mediator = mediator;
		}

		[HttpGet, LoadModelState]
		public async Task<IActionResult> Index(int id)
		{
			var departments = await Mediator.Send(new GetDepartmentsQuery { SelectedId = id });
			return View(departments);
		}

		[HttpPost, ValidateAntiForgeryToken, SaveModelState]
		public async Task<IActionResult> Insert([Bind("Name", "Description", "Level")] DepartmentViewModel viewmodel)
		{
			try
			{
				var response = await Mediator.Send(new InsertDepartmentCommand { Name = viewmodel.Name, Description = viewmodel.Description, Level = viewmodel.Level });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Error", ex.Message);
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpPost, ValidateAntiForgeryToken, SaveModelState]
		public async Task<IActionResult> Update(DepartmentViewModel viewmodel)
		{
			try
			{
				var response = await Mediator.Send(new UpdateDepartmentCommand { Id = viewmodel.Id, Name = viewmodel.Name, Description = viewmodel.Description, Level = viewmodel.Level, Version = viewmodel.RowVersion });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Error", ex.Message);
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpPost, ValidateAntiForgeryToken, SaveModelState]
		public async Task<IActionResult> Delete(DepartmentViewModel viewmodel)
		{
			try
			{
				var response = await Mediator.Send(new DeleteDepartmentCommand { Id = viewmodel.Id });
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Error", ex.Message);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}