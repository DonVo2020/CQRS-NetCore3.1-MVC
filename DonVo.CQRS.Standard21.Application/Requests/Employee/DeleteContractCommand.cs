using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Employee
{
	public class DeleteContractCommand : IRequest
	{
		public int Id { get; set; }
	}
}