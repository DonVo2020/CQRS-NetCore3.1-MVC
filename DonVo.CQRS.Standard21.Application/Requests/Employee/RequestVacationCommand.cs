using System;
using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Employee
{
	public class RequestVacationCommand : IRequest
	{
		public int EmployeeId { get; set; }
		public DateTimeOffset Start { get; set; }
		public DateTimeOffset End { get; set; }
		public int VacationTypeId { get; set; }
	}
}