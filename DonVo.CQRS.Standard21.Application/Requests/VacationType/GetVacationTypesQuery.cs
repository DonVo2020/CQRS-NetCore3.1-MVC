using DonVo.CQRS.Standard21.Application.ViewModels.VacationType;
using MediatR;

using System.Collections.Generic;

namespace DonVo.CQRS.Standard21.Application.Requests.VacationType
{
    public class GetVacationTypesQuery : IRequest<IEnumerable<VacationTypeViewModel>>
    {
        public int SelectedId { get; set; }
    }
}