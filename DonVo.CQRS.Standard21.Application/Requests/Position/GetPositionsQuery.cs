using DonVo.CQRS.Standard21.Application.ViewModels.Position;
using MediatR;

using System.Collections.Generic;

namespace DonVo.CQRS.Standard21.Application.Requests.Position
{
    public class GetPositionsQuery : IRequest<IEnumerable<PositionViewModel>>
	{
		public int SelectedId { get; set; }
	}
}