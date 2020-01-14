using DonVo.CQRS.Standard21.Application.Events.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Domain.Model.Identity;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Identity;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IMediator Mediator;
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IPositionRepository PositionRepository;
        private readonly IDepartmentRepository DepartmentRepository;
        private readonly IVacationTypeRepository VacationTypeRepository;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;

        public DeleteEmployeeHandler(IMediator mediator, IEmployeeRepository employeerepository, IPositionRepository positionrepository, IDepartmentRepository departmentrepository, IVacationTypeRepository vacationtyperepository, UserManager<ApplicationUser> usermanager, RoleManager<ApplicationRole> rolemanager)
        {
            Mediator = mediator;
            EmployeeRepository = employeerepository;
            PositionRepository = positionrepository;
            DepartmentRepository = departmentrepository;
            VacationTypeRepository = vacationtyperepository;
            UserManager = usermanager;
            RoleManager = rolemanager;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var user = await UserManager.FindByIdAsync(request.Id.ToString());
            var employees = await EmployeeRepository.Select();
            var employee = await EmployeeRepository.Get(request.Id);
            if (employee == null) throw new ArgumentOutOfRangeException("Employee does not exist.");

            foreach (var subordinate in employees.Where(e => e.Manager != null && e.Manager.Equals(employee)))
            {
                subordinate.UpdateManager(employee.Manager, subordinate.RowVersion);
                await EmployeeRepository.Update(subordinate);
            }

            await EmployeeRepository.Delete(employee);
            await UserManager.DeleteAsync(user);

            await Mediator.Publish(new EmployeeDeletedEvent { Id = employee.Id });

            return await Unit.Task;
        }
    }
}