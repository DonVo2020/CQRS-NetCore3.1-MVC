using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public class Vacation : Entity
	{
		[ForeignKey("EmployeeId"), Required]
		public int EmployeeId { get; protected set; }

		[NotMapped]
		public string EmployeeFullName { get; set; }

		[Required]
		public DateTimeOffset StartDate { get; protected set; }

		[Required]
		public DateTimeOffset EndDate { get; protected set; }

		[ForeignKey("VacationType"), Required]
		public int VacationTypeId { get; protected set; }

		[ForeignKey("VacationTypeId"), Required]
		public VacationType VacationType { get; protected set; }

		[ForeignKey("VacationStatus"), Required]
		public int VacationStatusId { get; protected set; }

		[NotMapped]
		public VacationState Status => (VacationState)VacationStatusId;

		protected Vacation() { }

		public Vacation(int employee, string fullname, DateTimeOffset startdate, DateTimeOffset enddate, VacationType type, VacationState status)
		{
			EmployeeId = employee;
			EmployeeFullName = fullname;
			StartDate = startdate;
			EndDate = enddate;
			VacationTypeId = type.Id;
			VacationType = type;
			VacationStatusId = (int)status;
			CreatedOn = DateTimeOffset.Now;
		}

		public int GetWorkingDaysAmount(IEnumerable<Holiday> holidays)
		{
			byte result = 0;
			DateTimeOffset date = StartDate;
			while (date <= EndDate)
			{
				if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !holidays.Any(d => d.Date.Year == date.Year && d.Date.Month == date.Month && d.Date.Day == date.Day))
					result++;

				date = date.AddDays(1);
			}
			return result;
		}

		public void Request()
		{
			if (Status != VacationState.Requested) throw new ArgumentOutOfRangeException("VacationStatus", "Invalid status");

			VacationStatusId = (int)VacationState.Requested;
			UpdatedOn = DateTimeOffset.Now;
		}

		public void Accept()
		{
			if (Status != VacationState.Requested) throw new ArgumentOutOfRangeException("VacationStatus", "Invalid status");

			VacationStatusId = (int)VacationState.Accepted;
			UpdatedOn = DateTimeOffset.Now;
		}

		public void Reject()
		{
			if (Status != VacationState.Requested) throw new ArgumentOutOfRangeException("VacationStatus", "Invalid status");

			VacationStatusId = (int)VacationState.Rejected;
			UpdatedOn = DateTimeOffset.Now;
		}

		public void Cancel()
		{
			if (Status == VacationState.Cancelled) throw new ArgumentOutOfRangeException("VacationStatus", "Invalid status");

			VacationStatusId = (int)VacationState.Cancelled;
			UpdatedOn = DateTimeOffset.Now;
		}

		public void Plan()
		{
			VacationStatusId = (int)VacationState.Planned;
			UpdatedOn = DateTimeOffset.Now;
		}
	}
}