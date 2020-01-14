using DonVo.CQRS.Standard21.Application.ViewModels.Holiday;

using MediatR;

using System.Collections.Generic;

namespace DonVo.CQRS.Standard21.Application.Requests.Holiday
{
    public class GetHolidaysQuery : IRequest<IEnumerable<HolidayViewModel>>
    {

    }
}