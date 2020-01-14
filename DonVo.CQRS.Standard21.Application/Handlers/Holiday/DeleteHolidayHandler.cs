using DonVo.CQRS.Standard21.Application.Events.Holiday;
using DonVo.CQRS.Standard21.Application.Requests.Holiday;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Holiday
{
    public class DeleteHolidayHandler : IRequestHandler<DeleteHolidayCommand>
    {
        private readonly IMediator Mediator;
        private readonly IHolidayRepository HolidayRepository;

        public DeleteHolidayHandler(IMediator mediator, IHolidayRepository holidayrepository)
        {
            Mediator = mediator;
            HolidayRepository = holidayrepository;
        }

        public async Task<Unit> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
        {
            var holiday = await HolidayRepository.Get(request.Id);
            if (holiday == null) throw new ArgumentOutOfRangeException("Holiday does not exist.");

            await HolidayRepository.Delete(holiday);

            await Mediator.Publish(new HolidayDeletedEvent { Id = holiday.Id });

            return await Unit.Task;
        }
    }
}