using DonVo.CQRS.Standard21.Application.Events.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class RequestVacationHandler : IRequestHandler<RequestVacationCommand>
    {
        private readonly IMediator Mediator;
        private readonly IVacationRepository VacationRepository;
        private readonly IVacationTypeRepository VacationTypeRepository;
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IHolidayRepository HolidayRepository;

        public RequestVacationHandler(IMediator mediator, IVacationRepository vacationrepository, IVacationTypeRepository vacationtyperepository, IEmployeeRepository employeerepository, IHolidayRepository holidayrepository)
        {
            Mediator = mediator;
            VacationRepository = vacationrepository;
            VacationTypeRepository = vacationtyperepository;
            EmployeeRepository = employeerepository;
            HolidayRepository = holidayrepository;
        }

        public async Task<Unit> Handle(RequestVacationCommand request, CancellationToken cancellationToken)
        {
            var employee = await EmployeeRepository.Get(request.EmployeeId);
            if (employee == null) throw new ArgumentOutOfRangeException("Employee does not exist.");

            var holidays = await HolidayRepository.Select();
            var vacationtype = await VacationTypeRepository.Get(request.VacationTypeId);
            var vacation = employee.InsertVacation(request.Start, request.End, vacationtype, DonVo.CQRS.Standard21.Domain.Model.Company.VacationState.Requested, holidays);

            await VacationRepository.Insert(vacation);

            await Mediator.Publish(new VacationRequestedEvent { Id = vacation.Id });

            return await Unit.Task;
        }
    }
}