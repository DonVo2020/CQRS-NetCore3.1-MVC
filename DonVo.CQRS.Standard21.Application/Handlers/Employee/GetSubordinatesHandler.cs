using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Application.ViewModels.Employee;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using DonVo.CQRS.Standard21.Helper;
using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class GetSubordinatesHandler : IRequestHandler<GetSubordinatesQuery, IEnumerable<EmployeeLiteViewModel>>
    {
        private readonly IMediator Mediator;
        private readonly IEmployeeRepository EmployeeRepository;

        public GetSubordinatesHandler(IMediator mediator, IEmployeeRepository employeerepository)
        {
            Mediator = mediator;
            EmployeeRepository = employeerepository;
        }

        public async Task<IEnumerable<EmployeeLiteViewModel>> Handle(GetSubordinatesQuery request, CancellationToken cancellationToken)
        {
            var employees = await EmployeeRepository.Select();
            var viewmodel = new List<EmployeeLiteViewModel>();
            foreach (var item in new CompanyHierarchyHelper().GetSubordinates(request.EmployeeId, employees))
            {
                var vm = new EmployeeLiteViewModel();
                vm.LoadFromDomain(item);
                viewmodel.Add(vm);
            }
            return viewmodel;
        }
    }
}