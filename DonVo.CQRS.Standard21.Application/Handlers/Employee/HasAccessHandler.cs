using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Domain.Model.Identity;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using DonVo.CQRS.Standard21.Helper;
using MediatR;

using Microsoft.AspNetCore.Identity;

using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class HasAccessHandler : IRequestHandler<HasAccessQuery, bool>
    {
        private readonly IMediator Mediator;
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly UserManager<ApplicationUser> UserManager;

        public HasAccessHandler(IMediator mediator, IEmployeeRepository employeerepository, UserManager<ApplicationUser> usermanager)
        {
            Mediator = mediator;
            EmployeeRepository = employeerepository;
            UserManager = usermanager;
        }

        public async Task<bool> Handle(HasAccessQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == request.EmployeeId)
                return true;

            var user = await UserManager.FindByIdAsync(request.UserId.ToString());
            var employee = await EmployeeRepository.Get(request.EmployeeId);
            if (employee == null || user == null)
                return false;

            if (await UserManager.IsInRoleAsync(user, Role.CompanyManagement.ToString()))
                return true;

            var employees = await EmployeeRepository.Select();

            return new CompanyHierarchyHelper().IsHigherInHierarchy(user.Id, employee.Id, employees);
        }
    }
}