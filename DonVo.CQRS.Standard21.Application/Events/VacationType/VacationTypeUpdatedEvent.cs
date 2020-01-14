using MediatR;

namespace DonVo.CQRS.Standard21.Application.Events.VacationType
{
	public class VacationTypeUpdatedEvent : INotification
	{
		public int Id { get; set; }
	}
}