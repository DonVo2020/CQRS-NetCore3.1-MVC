using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Employee
{
	public class HasAccessQuery : IRequest<bool>
	{
		public int UserId { get; set; }
		public int EmployeeId { get; set; }
	}
}