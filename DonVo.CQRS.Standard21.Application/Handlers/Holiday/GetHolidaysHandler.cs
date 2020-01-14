using DonVo.CQRS.Standard21.Application.Requests.Holiday;
using DonVo.CQRS.Standard21.Application.ViewModels.Holiday;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Holiday
{
    public class GetHolidaysHandler : IRequestHandler<GetHolidaysQuery, IEnumerable<HolidayViewModel>>
    {
        private readonly IMediator Mediator;
        private readonly IHolidayRepository HolidayRepository;

        public GetHolidaysHandler(IMediator mediator, IHolidayRepository holidayrepository)
        {
            Mediator = mediator;
            HolidayRepository = holidayrepository;
        }

        public async Task<IEnumerable<HolidayViewModel>> Handle(GetHolidaysQuery request, CancellationToken cancellationToken)
        {
            var holidays = await HolidayRepository.Select();
            var viewmodel = new List<HolidayViewModel>();
            foreach (var item in holidays)
            {
                var vm = new HolidayViewModel();
                vm.LoadFromDomain(item);
                viewmodel.Add(vm);
            }
            return viewmodel.OrderBy(item => item.Date).ToList();
        }
    }
}