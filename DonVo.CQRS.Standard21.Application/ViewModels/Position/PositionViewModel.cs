using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Position
{
    public class PositionViewModel
    {
        public int Id { get; set; }

        [StringLength(32), Required]
        public string Name { get; set; }

        [StringLength(256), Required]
        public string Description { get; set; }

        [Range(-127, 127), Required]
        public int Grade { get; set; }

        public string RowVersion { get; set; }

        public bool IsSelected { get; set; }

        public void LoadFromDomain(DonVo.CQRS.Standard21.Domain.Model.Company.Position entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Grade = entity.Grade;
            RowVersion = Convert.ToBase64String(entity.RowVersion);
        }
    }
}