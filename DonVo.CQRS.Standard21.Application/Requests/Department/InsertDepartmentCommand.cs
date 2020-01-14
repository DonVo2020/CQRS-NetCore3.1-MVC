using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Department
{
	public class InsertDepartmentCommand : IRequest<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Level { get; set; }
	}
}