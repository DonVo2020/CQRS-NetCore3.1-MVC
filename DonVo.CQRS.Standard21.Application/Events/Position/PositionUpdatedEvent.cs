using MediatR;

namespace DonVo.CQRS.Standard21.Application.Events.Position
{
	public class PositionUpdatedEvent : INotification
	{
		public int Id { get; set; }
	}
}