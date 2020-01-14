using DonVo.CQRS.Standard21.Application.Events.Position;
using DonVo.CQRS.Standard21.Application.Requests.Position;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Position
{
    public class UpdatePositionHandler : IRequestHandler<UpdatePositionCommand>
    {
        private readonly IMediator Mediator;
        private readonly IPositionRepository PositionRepository;

        public UpdatePositionHandler(IMediator mediator, IPositionRepository positionrepository)
        {
            Mediator = mediator;
            PositionRepository = positionrepository;
        }

        public async Task<Unit> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var position = await PositionRepository.Get(request.Id);
            if (position == null) throw new ArgumentOutOfRangeException("Position does not exist.");
            position.Update(request.Name, request.Description, request.Grade, Convert.FromBase64String(request.Version));
            await PositionRepository.Update(position);

            await Mediator.Publish(new PositionUpdatedEvent { Id = position.Id });

            return await Unit.Task;
        }
    }
}