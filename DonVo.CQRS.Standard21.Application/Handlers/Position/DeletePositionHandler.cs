using DonVo.CQRS.Standard21.Application.Events.Position;
using DonVo.CQRS.Standard21.Application.Requests.Position;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Position
{
    public class DeletePositionHandler : IRequestHandler<DeletePositionCommand>
    {
        private readonly IMediator Mediator;
        private readonly IPositionRepository PositionRepository;
        private readonly IEmployeeRepository EmployeeRepository;

        public DeletePositionHandler(IMediator mediator, IPositionRepository positionrepository, IEmployeeRepository employeerepository)
        {
            Mediator = mediator;
            PositionRepository = positionrepository;
            EmployeeRepository = employeerepository;
        }

        public async Task<Unit> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            var position = await PositionRepository.Get(request.Id);
            if (position == null) throw new ArgumentException("Position does not exist.");

            var employees = await EmployeeRepository.Select();
            if (employees.Any(e => e.Position.Equals(position))) throw new ArgumentOutOfRangeException("Position", "Some employees are in this position.");

            await PositionRepository.Delete(position);

            await Mediator.Publish(new PositionDeletedEvent { Id = position.Id });

            return await Unit.Task;
        }
    }
}