using MediatR;

namespace DonVo.CQRS.Standard21.Application.Events.Employee
{
	public class VacationRequestedEvent : INotification
	{
		public int Id { get; set; }
	}
}