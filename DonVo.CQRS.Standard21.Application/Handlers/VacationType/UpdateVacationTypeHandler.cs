using DonVo.CQRS.Standard21.Application.Events.VacationType;
using DonVo.CQRS.Standard21.Application.Requests.VacationType;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.VacationType
{
    public class UpdateVacationTypeHandler : IRequestHandler<UpdateVacationTypeCommand>
    {
        private readonly IMediator Mediator;
        private readonly IVacationTypeRepository VacationTypeRepository;

        public UpdateVacationTypeHandler(IMediator mediator, IVacationTypeRepository vacationtyperepository)
        {
            Mediator = mediator;
            VacationTypeRepository = vacationtyperepository;
        }

        public async Task<Unit> Handle(UpdateVacationTypeCommand request, CancellationToken cancellationToken)
        {
            var vacationtype = await VacationTypeRepository.Get(request.Id);
            if (vacationtype == null) throw new ArgumentOutOfRangeException("Vacation Type does not exist.");

            var pool = await VacationTypeRepository.Get(request.PoolId.GetValueOrDefault(0));
            vacationtype.Update(request.Name, request.DefaultLeaveDays, request.IsPassing, pool, Convert.FromBase64String(request.Version));

            await VacationTypeRepository.Update(vacationtype);

            await Mediator.Publish(new VacationTypeUpdatedEvent { Id = vacationtype.Id });

            return await Unit.Task;
        }
    }
}