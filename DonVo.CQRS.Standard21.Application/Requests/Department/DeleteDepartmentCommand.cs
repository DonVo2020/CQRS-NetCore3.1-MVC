using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Department
{
	public class DeleteDepartmentCommand : IRequest
	{
		public int Id { get; set; }
	}
}