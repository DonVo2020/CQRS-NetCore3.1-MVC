using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Position
{
	public class UpdatePositionCommand : IRequest
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Grade { get; set; }
		public string Version { get; set; }
	}
}