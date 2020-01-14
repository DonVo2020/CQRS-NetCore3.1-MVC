using DonVo.CQRS.Standard21.Application.Events.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class RejectVacationHandler : IRequestHandler<RejectVacationCommand>
    {
        private readonly IMediator Mediator;
        private readonly IVacationRepository VacationRepository;

        public RejectVacationHandler(IMediator mediator, IVacationRepository vacationrepository)
        {
            Mediator = mediator;
            VacationRepository = vacationrepository;
        }

        public async Task<Unit> Handle(RejectVacationCommand request, CancellationToken cancellationToken)
        {
            var vacation = await VacationRepository.Get(request.Id);
            if (vacation == null) throw new ArgumentOutOfRangeException("Vacation does not exist.");

            vacation.Reject();

            await VacationRepository.Update(vacation);

            await Mediator.Publish(new VacationRejectedEvent { Id = vacation.Id });

            return await Unit.Task;
        }
    }
}