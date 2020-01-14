using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.VacationType
{
	public class InsertVacationTypeCommand : IRequest<int>
	{
		public string Name { get; set; }
		public int DefaultLeaveDays { get; set; }
		public bool IsPassing { get; set; }
		public int? PoolId { get; set; }
	}
}