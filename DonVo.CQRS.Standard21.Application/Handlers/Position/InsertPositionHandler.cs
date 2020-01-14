using DonVo.CQRS.Standard21.Application.Events.Position;
using DonVo.CQRS.Standard21.Application.Requests.Position;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Position
{
    public class InsertPositionHandler : IRequestHandler<InsertPositionCommand, int>
    {
        private readonly IMediator Mediator;
        private readonly IPositionRepository PositionRepository;

        public InsertPositionHandler(IMediator mediator, IPositionRepository positionrepository)
        {
            Mediator = mediator;
            PositionRepository = positionrepository;
        }

        public async Task<int> Handle(InsertPositionCommand request, CancellationToken cancellationToken)
        {
            var position = new DonVo.CQRS.Standard21.Domain.Model.Company.Position(request.Name, request.Description, request.Grade);
            var id = await PositionRepository.Insert(position);

            await Mediator.Publish(new PositionInsertedEvent { Id = position.Id });

            return id;
        }
    }
}