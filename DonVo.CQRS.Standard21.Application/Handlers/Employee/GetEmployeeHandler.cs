using DonVo.CQRS.Standard21.Application.Events.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Application.Requests.VacationType;
using DonVo.CQRS.Standard21.Application.ViewModels.Employee;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, EmployeeViewModel>
    {
        private readonly IMediator Mediator;
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IHolidayRepository HolidayRepository;

        public GetEmployeeHandler(IMediator mediator, IEmployeeRepository employeerepository, IHolidayRepository holidayrepository)
        {
            Mediator = mediator;
            EmployeeRepository = employeerepository;
            HolidayRepository = holidayrepository;
        }

        public async Task<EmployeeViewModel> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await EmployeeRepository.Get(request.Id);
            var holidays = await HolidayRepository.Select();
            var viewmodel = new EmployeeViewModel();
            viewmodel.LoadFromDomain(employee, holidays, request.Year.GetValueOrDefault(DateTimeOffset.Now.Year));

            var subordinates = await Mediator.Send(new GetSubordinatesQuery { EmployeeId = request.Id });
            viewmodel.Subordinates = subordinates.ToList();

            var requests = request.MoreVacationRequests ? await Mediator.Send(new GetSubSubordinateRequestsQuery { EmployeeId = request.Id }) : await Mediator.Send(new GetSubordinateRequestsQuery { EmployeeId = request.Id });
            viewmodel.SubordinateRequests = requests.ToList();

            var vacationtypes = await Mediator.Send(new GetVacationTypesQuery());
            viewmodel.VacationTypes = vacationtypes.ToList();

            return viewmodel;
        }
    }
}