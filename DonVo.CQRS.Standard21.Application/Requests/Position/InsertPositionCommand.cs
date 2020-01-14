using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Position
{
	public class InsertPositionCommand : IRequest<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Grade { get; set; }
	}
}