using DonVo.CQRS.Standard21.Application.Events.Department;
using DonVo.CQRS.Standard21.Application.Requests.Department;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Department
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDepartmentRepository DepartmentRepository;
        private readonly IEmployeeRepository EmployeeRepository;

        public DeleteDepartmentHandler(IMediator mediator, IDepartmentRepository departmentrepository, IEmployeeRepository employeerepository)
        {
            Mediator = mediator;
            DepartmentRepository = departmentrepository;
            EmployeeRepository = employeerepository;
        }

        public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await DepartmentRepository.Get(request.Id);
            if (department == null) throw new ArgumentException("Department does not exist.");

            var employees = await EmployeeRepository.Select();
            if (employees.Any(e => e.Department.Equals(department))) throw new ArgumentOutOfRangeException("Department", "Some employees are in this department.");

            await DepartmentRepository.Delete(department);

            await Mediator.Publish(new DepartmentDeletedEvent { Id = department.Id });

            return await Unit.Task;
        }
    }
}