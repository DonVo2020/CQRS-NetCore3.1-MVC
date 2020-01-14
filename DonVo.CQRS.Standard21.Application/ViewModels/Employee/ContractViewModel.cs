using DonVo.CQRS.Standard21.Domain.Model.Company;
using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Employee
{
    public class ContractViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Start Date"), Required]
        public DateTimeOffset StartDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "End Date")]
        public DateTimeOffset? EndDate { get; set; }

        [StringLength(16), Display(Name = "Type"), Required]
        public string ContractType { get; set; }

        [Range(0, 9999999), Display(Name = "Remuneration"), Required]
        public decimal Remuneration { get; set; }

        public string RowVersion { get; set; }

        public void LoadFromDomain(Contract entity)
        {
            Id = entity.Id;
            EmployeeId = entity.EmployeeId;
            StartDate = entity.StartDate;
            EndDate = entity.EndDate;
            ContractType = entity.ContractType.Name;
            Remuneration = entity.Remuneration;
            RowVersion = Convert.ToBase64String(entity.RowVersion);
        }
    }
}