using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Employee
{
	public class AcceptVacationCommand : IRequest
	{
		public int Id { get; set; }
	}
}