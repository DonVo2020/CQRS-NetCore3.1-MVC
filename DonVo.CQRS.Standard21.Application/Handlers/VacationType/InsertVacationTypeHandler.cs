using DonVo.CQRS.Standard21.Application.Events.VacationType;
using DonVo.CQRS.Standard21.Application.Requests.VacationType;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.VacationType
{
    public class InsertVacationTypeHandler : IRequestHandler<InsertVacationTypeCommand, int>
    {
        private readonly IMediator Mediator;
        private readonly IVacationTypeRepository VacationTypeRepository;

        public InsertVacationTypeHandler(IMediator mediator, IVacationTypeRepository vacationtyperepository)
        {
            Mediator = mediator;
            VacationTypeRepository = vacationtyperepository;
        }

        public async Task<int> Handle(InsertVacationTypeCommand request, CancellationToken cancellationToken)
        {
            var pool = await VacationTypeRepository.Get(request.PoolId.GetValueOrDefault(0));
            var vacationtype = new DonVo.CQRS.Standard21.Domain.Model.Company.VacationType(request.Name, request.DefaultLeaveDays, request.IsPassing, pool);
            var id = await VacationTypeRepository.Insert(vacationtype);

            await Mediator.Publish(new VacationTypeInsertedEvent { Id = vacationtype.Id });

            return id;
        }
    }
}