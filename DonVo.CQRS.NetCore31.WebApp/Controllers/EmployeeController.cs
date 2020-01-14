using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Application.ViewModels.Employee;
using DonVo.CQRS.Standard21.Domain.Model.Identity;
using DonVo.CQRS.NetCore31.WebApp.Infrastructure;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.CQRS.NetCore31.WebApp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IMediator Mediator;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;

        public EmployeeController(IMediator mediator, UserManager<ApplicationUser> usermanager, RoleManager<ApplicationRole> rolemanager)
        {
            Mediator = mediator;
            UserManager = usermanager;
            RoleManager = rolemanager;
        }

        [HttpGet, Authorize(Roles = "CompanyManagement")]
        public async Task<IActionResult> Index()
        {
            var employees = await Mediator.Send(new GetEmployeesQuery());
            return View(employees);
        }

        [HttpGet, LoadModelState]
        public async Task<IActionResult> Person(int? id, int? year, bool more = false)
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            if (id.HasValue)
                if (!await Mediator.Send(new HasAccessQuery { UserId = user.Id, EmployeeId = id.Value }))
                    return Unauthorized();

            if (!id.HasValue) id = user.Id;
            if (!year.HasValue) year = DateTimeOffset.Now.Year;

            var employee = await Mediator.Send(new GetEmployeeQuery { Id = id.Value, Year = year, MoreVacationRequests = more });

            return View(employee);
        }

        [HttpGet, Authorize(Roles = "CompanyManagement"), LoadModelState]
        public async Task<IActionResult> Update(int id)
        {
            var employee = await Mediator.Send(new GetEmployeeRichQuery { Id = id });
            return View(employee);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "CompanyManagement"), SaveModelState]
        public async Task<IActionResult> Update(EmployeeRichViewModel viewmodel)
        {
            try
            {
                if (viewmodel.Id != 0)
                {
                    await Mediator.Send(new UpdateEmployeeCommand { Id = viewmodel.Id, FirstName = viewmodel.FirstName, LastName = viewmodel.LastName, BirthDate = viewmodel.BirthDate, Gender = viewmodel.Gender.Equals("Male") ? true : false, Email = viewmodel.Email, Phone = viewmodel.Phone, Street = viewmodel.Street, PostalCode = viewmodel.PostalCode, City = viewmodel.City, PositionId = viewmodel.PositionId, DepartmentId = viewmodel.DepartmentId, ManagerId = viewmodel.ManagerId, VacationDays = viewmodel.VacationTypes.ToDictionary(k => k.Id, v => v.Value), Roles = viewmodel.Roles.Where(r => r.Granted).Select(r => r.Name), Version = viewmodel.RowVersion });
                }
                else
                {
                    viewmodel.Id = await Mediator.Send(new InsertEmployeeCommand { FirstName = viewmodel.FirstName, LastName = viewmodel.LastName, BirthDate = viewmodel.BirthDate, Gender = viewmodel.Gender.Equals("Male") ? true : false, Email = viewmodel.Email, Phone = viewmodel.Phone, Street = viewmodel.Street, PostalCode = viewmodel.PostalCode, City = viewmodel.City, PositionId = viewmodel.PositionId, DepartmentId = viewmodel.DepartmentId, ManagerId = viewmodel.ManagerId, VacationDays = viewmodel.VacationTypes.ToDictionary(k => k.Id, v => v.Value), Roles = viewmodel.Roles.Where(r => r.Granted).Select(r => r.Name) });
                }
                return RedirectToAction(nameof(Person), new { id = viewmodel.Id, year = DateTimeOffset.Now.Year });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return View(viewmodel);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "CompanyManagement")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteEmployeeCommand { Id = id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "VacationManagement"), SaveModelState]
        public async Task<IActionResult> RequestVacation(VacationViewModel viewmodel)
        {
            try
            {
                await Mediator.Send(new RequestVacationCommand { EmployeeId = viewmodel.EmployeeId, Start = viewmodel.StartDate, End = viewmodel.EndDate, VacationTypeId = viewmodel.VacationTypeId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Person));
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "VacationManagement")]
        public async Task<IActionResult> AcceptVacation(VacationViewModel viewmodel)
        {
            try
            {
                await Mediator.Send(new AcceptVacationCommand { Id = viewmodel.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Person));
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "VacationManagement")]
        public async Task<IActionResult> RejectVacation(VacationViewModel viewmodel)
        {
            try
            {
                await Mediator.Send(new RejectVacationCommand { Id = viewmodel.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Person));
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "VacationManagement")]
        public async Task<IActionResult> CancelVacation(VacationViewModel viewmodel)
        {
            try
            {
                await Mediator.Send(new CancelVacationCommand { Id = viewmodel.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Person));
        }

        [HttpGet, Authorize(Roles = "CompanyManagement")]
        public async Task<IActionResult> InsertContract(int employeeid)
        {
            await Task.Delay(0);
            var viewmodel = new ContractViewModel
            {
                EmployeeId = employeeid,
                StartDate = DateTimeOffset.Now
            };
            return View(viewmodel);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "CompanyManagement")]
        public async Task<IActionResult> InsertContract(ContractViewModel viewmodel)
        {
            try
            {
                var employee = await Mediator.Send(new InsertContractCommand { EmployeeId = viewmodel.EmployeeId, Start = viewmodel.StartDate, End = viewmodel.EndDate, Remuneration = viewmodel.Remuneration, ContractType = viewmodel.ContractType });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Person), new { id = viewmodel.EmployeeId, year = DateTimeOffset.Now.Year });
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "CompanyManagement")]
        public async Task<IActionResult> DeleteContract(ContractViewModel viewmodel)
        {
            try
            {
                await Mediator.Send(new DeleteContractCommand { Id = viewmodel.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return RedirectToAction(nameof(Person), new { id = viewmodel.EmployeeId, year = DateTimeOffset.Now.Year });
        }
    }
}