using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public class AvailableVacationDays : Entity
	{
		[ForeignKey("EmployeeId"), Required]
		public int EmployeeId { get; protected set; }

		[Range(0, 9999), Required]
		public int Year { get; protected set; }

		[ForeignKey("VacationType"), Required]
		public int VacationTypeId { get; protected set; }

		[ForeignKey("VacationTypeId"), Required]
		public VacationType VacationType { get; protected set; }

		[Range(0, 255), Required]
		public int Value { get; protected set; }

		protected AvailableVacationDays() { }

		public AvailableVacationDays(int employee, int year, VacationType type, int value)
		{
			EmployeeId = employee;
			Year = year;
			VacationTypeId = type.Id;
			VacationType = type;
			Value = value;
			CreatedOn = DateTimeOffset.Now;
		}

		public void Update(int value)
		{
			Value = value;
			UpdatedOn = DateTimeOffset.Now;
		}
	}
}