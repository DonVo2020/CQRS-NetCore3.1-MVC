using DonVo.CQRS.Standard21.Application.Requests.Department;
using DonVo.CQRS.Standard21.Application.ViewModels.Department;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Department
{
    public class GetDepartmentsHandler : IRequestHandler<GetDepartmentsQuery, IEnumerable<DepartmentViewModel>>
    {
        private readonly IMediator Mediator;
        private readonly IDepartmentRepository DepartmentRepository;

        public GetDepartmentsHandler(IMediator mediator, IDepartmentRepository departmentrepository)
        {
            Mediator = mediator;
            DepartmentRepository = departmentrepository;
        }

        public async Task<IEnumerable<DepartmentViewModel>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await DepartmentRepository.Select();
            var viewmodel = new List<DepartmentViewModel>();
            foreach (var item in departments)
            {
                var vm = new DepartmentViewModel();
                vm.LoadFromDomain(item);
                vm.IsSelected = item.Id == request.SelectedId;
                viewmodel.Add(vm);
            }
            return viewmodel.OrderBy(item => item.Level).ThenBy(item => item.Name).ToList();
        }
    }
}