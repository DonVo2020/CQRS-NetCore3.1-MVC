using DonVo.CQRS.Standard21.Application.Events.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class AcceptVacationHandler : IRequestHandler<AcceptVacationCommand>
    {
        private readonly IMediator Mediator;
        private readonly IVacationRepository VacationRepository;

        public AcceptVacationHandler(IMediator mediator, IVacationRepository vacationrepository)
        {
            Mediator = mediator;
            VacationRepository = vacationrepository;
        }

        public async Task<Unit> Handle(AcceptVacationCommand request, CancellationToken cancellationToken)
        {
            var vacation = await VacationRepository.Get(request.Id);
            if (vacation == null) throw new ArgumentOutOfRangeException("Vacation does not exist.");

            vacation.Accept();

            await VacationRepository.Update(vacation);

            await Mediator.Publish(new VacationAcceptedEvent { Id = vacation.Id });

            return await Unit.Task;
        }
    }
}