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
    public class GetSubordinateRequestsHandler : IRequestHandler<GetSubordinateRequestsQuery, IEnumerable<VacationViewModel>>
    {
        private readonly IMediator Mediator;
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IHolidayRepository HolidayRepository;

        public GetSubordinateRequestsHandler(IMediator mediator, IEmployeeRepository employeerepository, IHolidayRepository holidayrepository)
        {
            Mediator = mediator;
            EmployeeRepository = employeerepository;
            HolidayRepository = holidayrepository;
        }

        public async Task<IEnumerable<VacationViewModel>> Handle(GetSubordinateRequestsQuery request, CancellationToken cancellationToken)
        {
            var employees = await EmployeeRepository.Select();
            var holidays = await HolidayRepository.Select();
            var requests = new CompanyHierarchyHelper().GetSubordinateRequests(request.EmployeeId, employees, holidays);

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