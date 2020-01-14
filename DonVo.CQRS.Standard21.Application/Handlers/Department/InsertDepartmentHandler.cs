using DonVo.CQRS.Standard21.Application.Events.Department;
using DonVo.CQRS.Standard21.Application.Requests.Department;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Department
{
    public class InsertDepartmentHandler : IRequestHandler<InsertDepartmentCommand, int>
    {
        private readonly IMediator Mediator;
        private readonly IDepartmentRepository DepartmentRepository;

        public InsertDepartmentHandler(IMediator mediator, IDepartmentRepository departmentrepository)
        {
            Mediator = mediator;
            DepartmentRepository = departmentrepository;
        }

        public async Task<int> Handle(InsertDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new DonVo.CQRS.Standard21.Domain.Model.Company.Department(request.Name, request.Description, request.Level);
            var id = await DepartmentRepository.Insert(department);

            await Mediator.Publish(new DepartmentInsertedEvent { Id = department.Id });

            return id;
        }
    }
}