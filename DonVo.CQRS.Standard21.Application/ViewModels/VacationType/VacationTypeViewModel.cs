using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Application.ViewModels.VacationType
{
    public class VacationTypeViewModel
    {
        public int Id { get; set; }

        [StringLength(32), Display(Name = "Name"), Required]
        public string Name { get; set; }

        [Range(0, 255), Display(Name = "Default Leave Days"), Required]
        public int DefaultLeaveDays { get; set; }

        [Display(Name = "Is Passing"), Required]
        public bool IsPassing { get; set; }

        [Display(Name = "Pool")]
        public int? PoolId { get; set; }

        [Display(Name = "Pool")]
        public string PoolName { get; set; }

        public string RowVersion { get; set; }

        public bool IsSelected { get; set; }

        public int Value { get; set; }

        public IList<VacationTypeViewModel> VacationTypes { get; set; }

        public void LoadFromDomain(DonVo.CQRS.Standard21.Domain.Model.Company.VacationType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            DefaultLeaveDays = entity.DefaultLeaveDays;
            IsPassing = entity.IsPassing;
            PoolId = entity.Pool?.Id;
            PoolName = entity.Pool?.Name;
            RowVersion = Convert.ToBase64String(entity.RowVersion);
        }
    }
}