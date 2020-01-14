using DonVo.CQRS.Standard21.Application.Events.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Domain.Model.Identity;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class InsertEmployeeHandler : IRequestHandler<InsertEmployeeCommand, int>
    {
        private readonly IMediator Mediator;
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IPositionRepository PositionRepository;
        private readonly IDepartmentRepository DepartmentRepository;
        private readonly IVacationTypeRepository VacationTypeRepository;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<ApplicationRole> RoleManager;

        public InsertEmployeeHandler(IMediator mediator, IEmployeeRepository employeerepository, IPositionRepository positionrepository, IDepartmentRepository departmentrepository, IVacationTypeRepository vacationtyperepository, UserManager<ApplicationUser> usermanager, RoleManager<ApplicationRole> rolemanager)
        {
            Mediator = mediator;
            EmployeeRepository = employeerepository;
            PositionRepository = positionrepository;
            DepartmentRepository = departmentrepository;
            VacationTypeRepository = vacationtyperepository;
            UserManager = usermanager;
            RoleManager = rolemanager;
        }

        public async Task<int> Handle(InsertEmployeeCommand request, CancellationToken cancellationToken)
        {
            var vacationtypes = await VacationTypeRepository.Select();
            var vacationdays = new Dictionary<DonVo.CQRS.Standard21.Domain.Model.Company.VacationType, int>();
            foreach (var item in request.VacationDays)
                vacationdays.Add(vacationtypes.Single(t => t.Id == item.Key), item.Value);

            var position = await PositionRepository.Get(request.PositionId);
            if (position == null) throw new ArgumentOutOfRangeException("Position does not exist.");

            var department = await DepartmentRepository.Get(request.DepartmentId);
            if (department == null) throw new ArgumentOutOfRangeException("Department does not exist.");

            var manager = await EmployeeRepository.Get(request.ManagerId.GetValueOrDefault(0));

            var employee = new DonVo.CQRS.Standard21.Domain.Model.Company.Employee
            (
                vacationdays,
                request.FirstName,
                request.LastName,
                request.BirthDate,
                request.Gender,
                request.Email,
                request.Phone,
                request.Street,
                request.PostalCode,
                request.City,
                position,
                department,
                manager
            );

            var id = await EmployeeRepository.Insert(employee);

            var user = await UserManager.FindByIdAsync(employee.ApplicationUserId.ToString());
            await UserManager.AddToRolesAsync(user, request.Roles);

            await Mediator.Publish(new EmployeeInsertedEvent { Id = employee.Id });

            return id;
        }
    }
}