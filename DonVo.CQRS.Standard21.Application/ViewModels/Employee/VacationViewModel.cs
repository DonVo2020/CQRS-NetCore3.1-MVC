using DonVo.CQRS.Standard21.Application.ViewModels.VacationType;
using DonVo.CQRS.Standard21.Domain.Model.Company;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Employee
{
    public class VacationViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Employee")]
        public string EmployeeName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Start Date"), Required]
        public DateTimeOffset StartDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "End Date"), Required]
        public DateTimeOffset EndDate { get; set; }

        [Display(Name = "Type"), Required]
        public int VacationTypeId { get; set; }

        [StringLength(16), Display(Name = "Type")]
        public string VacationType { get; set; }

        [StringLength(16), Display(Name = "Status"), Required]
        public string VacationStatus { get; set; }

        [Display(Name = "Working Days")]
        public int WorkingDaysAmount { get; set; }

        public string RowVersion { get; set; }

        public IList<VacationTypeViewModel> VacationTypes { get; set; }

        public void LoadFromDomain(Vacation entity, IEnumerable<DonVo.CQRS.Standard21.Domain.Model.Company.Holiday> holidays)
        {
            Id = entity.Id;
            EmployeeId = entity.EmployeeId;
            EmployeeName = entity.EmployeeFullName;
            StartDate = entity.StartDate;
            EndDate = entity.EndDate;
            VacationTypeId = entity.VacationTypeId;
            VacationType = entity.VacationType.Name;
            VacationStatus = entity.Status.ToString();
            WorkingDaysAmount = entity.GetWorkingDaysAmount(holidays);
            RowVersion = Convert.ToBase64String(entity.RowVersion);
        }
    }
}