using DonVo.CQRS.Standard21.Application.Events.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Department;
using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Position;
using DonVo.CQRS.Standard21.Application.Requests.VacationType;
using DonVo.CQRS.Standard21.Application.ViewModels.Employee;
using DonVo.CQRS.Standard21.Domain.Model.Identity;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class GetEmployeeRichHandler : IRequestHandler<GetEmployeeRichQuery, EmployeeRichViewModel>
    {
        private readonly IMediator Mediator;
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;

        public GetEmployeeRichHandler(IMediator mediator, IEmployeeRepository employeerepository, UserManager<ApplicationUser> usermanager, RoleManager<ApplicationRole> rolemanager)
        {
            Mediator = mediator;
            EmployeeRepository = employeerepository;
            UserManager = usermanager;
            RoleManager = rolemanager;
        }

        public async Task<EmployeeRichViewModel> Handle(GetEmployeeRichQuery request, CancellationToken cancellationToken)
        {
            var employee = await EmployeeRepository.Get(request.Id.GetValueOrDefault(0));
            var viewmodel = new EmployeeRichViewModel();
            viewmodel.LoadFromDomain(employee);

            var vacationtypes = await Mediator.Send(new GetVacationTypesQuery());
            viewmodel.VacationTypes = vacationtypes.ToList();
            viewmodel.ApplyVacationDays();

            var positions = await Mediator.Send(new GetPositionsQuery());
            viewmodel.Positions = positions.ToList();

            var departments = await Mediator.Send(new GetDepartmentsQuery());
            viewmodel.Departments = departments.ToList();

            var employeesvm = await Mediator.Send(new GetEmployeesQuery());
            viewmodel.Employees = employeesvm.Where(item => item.Id != employee?.Id && item.ManagerId != employee?.Id).ToList();

            var userroles = request.Id.GetValueOrDefault(0) > 0 ? await UserManager.GetRolesAsync(await UserManager.FindByIdAsync(request.Id.ToString())) : new List<string>();
            var roles = RoleManager.Roles.ToList();
            var rolesvm = new List<RoleViewModel>();
            foreach (var item in roles)
            {
                var rolevm = new RoleViewModel();
                rolevm.LoadFromDomain(item, userroles);
                rolesvm.Add(rolevm);
            }
            viewmodel.Roles = rolesvm;

            return viewmodel;
        }
    }
}