using DonVo.CQRS.Standard21.Application.Events.VacationType;
using DonVo.CQRS.Standard21.Application.Requests.VacationType;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.VacationType
{
    public class DeleteVacationTypeHandler : IRequestHandler<DeleteVacationTypeCommand>
    {
        private readonly IMediator Mediator;
        private readonly IVacationTypeRepository VacationTypeRepository;
        private readonly IEmployeeRepository EmployeeRepository;

        public DeleteVacationTypeHandler(IMediator mediator, IVacationTypeRepository vacationtyperepository, IEmployeeRepository employeerepository)
        {
            Mediator = mediator;
            VacationTypeRepository = vacationtyperepository;
            EmployeeRepository = employeerepository;
        }

        public async Task<Unit> Handle(DeleteVacationTypeCommand request, CancellationToken cancellationToken)
        {
            var vacationtype = await VacationTypeRepository.Get(request.Id);
            if (vacationtype == null) throw new ArgumentOutOfRangeException("Vacation Type does not exist.");

            var employees = await EmployeeRepository.Select();
            if (employees.Any(e => e.GetVacations(DateTimeOffset.Now.Year).Any(v => v.VacationType.Equals(vacationtype)))) throw new ArgumentOutOfRangeException("Some employees has active vacations of this type.");

            var vacationtypes = await VacationTypeRepository.Select();
            foreach (var type in vacationtypes.Where(e => e.Pool != null && e.Pool.Equals(vacationtype)))
            {
                type.UpdatePool(vacationtype.Pool, type.RowVersion);
                await VacationTypeRepository.Update(type);
            }

            await VacationTypeRepository.Delete(vacationtype);

            await Mediator.Publish(new VacationTypeDeletedEvent { Id = vacationtype.Id });

            return await Unit.Task;
        }
    }
}