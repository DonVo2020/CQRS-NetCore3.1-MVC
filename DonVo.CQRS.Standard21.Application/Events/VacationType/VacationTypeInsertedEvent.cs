using MediatR;

namespace DonVo.CQRS.Standard21.Application.Events.VacationType
{
	public class VacationTypeInsertedEvent : INotification
	{
		public int Id { get; set; }
	}
}