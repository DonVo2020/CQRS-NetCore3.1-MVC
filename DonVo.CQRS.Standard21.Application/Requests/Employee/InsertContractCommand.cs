using System;
using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Employee
{
	public class InsertContractCommand : IRequest
	{
		public int EmployeeId { get; set; }
		public DateTimeOffset Start { get; set; }
		public DateTimeOffset? End { get; set; }
		public string ContractType { get; set; }
		public decimal Remuneration { get; set; }
	}
}