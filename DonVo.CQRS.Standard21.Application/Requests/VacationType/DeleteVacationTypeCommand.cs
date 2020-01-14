using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.VacationType
{
	public class DeleteVacationTypeCommand : IRequest
	{
		public int Id { get; set; }
	}
}