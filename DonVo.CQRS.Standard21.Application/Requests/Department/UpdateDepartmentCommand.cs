using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Department
{
	public class UpdateDepartmentCommand : IRequest
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Level { get; set; }
		public string Version { get; set; }
	}
}