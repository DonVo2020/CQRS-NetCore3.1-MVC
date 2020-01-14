using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [StringLength(32), Required]
        public string Name { get; set; }

        [StringLength(256), Required]
        public string Description { get; set; }

        [Range(-127, 127), Required]
        public int Level { get; set; }

        public string RowVersion { get; set; }

        public bool IsSelected { get; set; }

        public void LoadFromDomain(DonVo.CQRS.Standard21.Domain.Model.Company.Department entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Level = entity.Level;
            RowVersion = Convert.ToBase64String(entity.RowVersion);
        }
    }
}