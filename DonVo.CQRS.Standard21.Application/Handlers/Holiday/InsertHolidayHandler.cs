using DonVo.CQRS.Standard21.Application.Events.Holiday;
using DonVo.CQRS.Standard21.Application.Requests.Holiday;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Holiday
{
    public class InsertHolidayHandler : IRequestHandler<InsertHolidayCommand, int>
    {
        private readonly IMediator Mediator;
        private readonly IHolidayRepository HolidayRepository;

        public InsertHolidayHandler(IMediator mediator, IHolidayRepository holidayrepository)
        {
            Mediator = mediator;
            HolidayRepository = holidayrepository;
        }

        public async Task<int> Handle(InsertHolidayCommand request, CancellationToken cancellationToken)
        {
            var holiday = new DonVo.CQRS.Standard21.Domain.Model.Company.Holiday(request.Date);
            var holidays = await HolidayRepository.Select();
            if (holidays.Any(h => h.Date.Year == holiday.Date.Year && h.Date.Month == holiday.Date.Month && h.Date.Day == holiday.Date.Day)) throw new ArgumentOutOfRangeException("Date", "Holiday already exist.");
            var id = await HolidayRepository.Insert(holiday);

            await Mediator.Publish(new HolidayInsertedEvent { Id = holiday.Id });

            return id;
        }
    }
}