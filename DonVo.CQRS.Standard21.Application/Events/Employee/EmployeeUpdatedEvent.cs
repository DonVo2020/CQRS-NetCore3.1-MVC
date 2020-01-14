using MediatR;

namespace DonVo.CQRS.Standard21.Application.Events.Employee
{
	public class EmployeeUpdatedEvent : INotification
	{
		public int Id { get; set; }
	}
}