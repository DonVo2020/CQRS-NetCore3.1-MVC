using MediatR;

namespace DonVo.CQRS.Standard21.Application.Events.Holiday
{
	public class HolidayDeletedEvent : INotification
	{
		public int Id { get; set; }
	}
}