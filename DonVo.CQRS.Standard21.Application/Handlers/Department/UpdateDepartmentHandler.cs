using DonVo.CQRS.Standard21.Application.Events.Department;
using DonVo.CQRS.Standard21.Application.Requests.Department;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Department
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDepartmentRepository DepartmentRepository;

        public UpdateDepartmentHandler(IMediator mediator, IDepartmentRepository departmentrepository)
        {
            Mediator = mediator;
            DepartmentRepository = departmentrepository;
        }

        public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await DepartmentRepository.Get(request.Id);
            if (department == null) throw new ArgumentOutOfRangeException("Department does not exist.");
            department.Update(request.Name, request.Description, request.Level, Convert.FromBase64String(request.Version));
            await DepartmentRepository.Update(department);

            await Mediator.Publish(new DepartmentUpdatedEvent { Id = department.Id });

            return await Unit.Task;
        }
    }
}