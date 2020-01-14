using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Holiday
{
	public class DeleteHolidayCommand : IRequest
	{
		public int Id { get; set; }
	}
}