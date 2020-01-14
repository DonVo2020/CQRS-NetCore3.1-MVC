using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Position
{
	public class DeletePositionCommand : IRequest
	{
		public int Id { get; set; }
	}
}