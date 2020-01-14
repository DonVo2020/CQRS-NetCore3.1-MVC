using System;
using System.Collections.Generic;
using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Employee
{
	public class InsertEmployeeCommand : IRequest<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTimeOffset BirthDate { get; set; }
		public bool Gender { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Street { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
		public int PositionId { get; set; }
		public int DepartmentId { get; set; }
		public int? ManagerId { get; set; }
		public Dictionary<int, int> VacationDays { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}