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
    public class GetSubSubordinateRequestsHandler : IRequestHandler<GetSubSubordinateRequestsQuery, IEnumerable<VacationViewModel>>
    {
        private readonly IMediator Mediator;
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IHolidayRepository HolidayRepository;

        public GetSubSubordinateRequestsHandler(IMediator mediator, IEmployeeRepository employeerepository, IHolidayRepository holidayrepository)
        {
            Mediator = mediator;
            EmployeeRepository = employeerepository;
            HolidayRepository = holidayrepository;
        }

        public async Task<IEnumerable<VacationViewModel>> Handle(GetSubSubordinateRequestsQuery request, CancellationToken cancellationToken)
        {
            var employees = await EmployeeRepository.Select();
            var holidays = await HolidayRepository.Select();
            var requests = new CompanyHierarchyHelper().GetSubSubordinateRequests(request.EmployeeId, employees, holidays);

            var viewmodel = new List<VacationViewModel>();
            foreach (var item in requests)
            {
                var vm = new VacationViewModel();
                vm.LoadFromDomain(item, holidays);
                viewmodel.Add(vm);
            }

            return viewmodel;
        }
    }
}