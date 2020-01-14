using System;
using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Holiday
{
	public class InsertHolidayCommand : IRequest<int>
	{
		public DateTimeOffset Date { get; set; }
	}
}