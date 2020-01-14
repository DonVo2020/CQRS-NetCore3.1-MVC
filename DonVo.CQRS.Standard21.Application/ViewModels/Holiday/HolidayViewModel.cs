using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Holiday
{
    public class HolidayViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Required]
        public DateTimeOffset Date { get; set; }

        public string RowVersion { get; set; }

        public void LoadFromDomain(DonVo.CQRS.Standard21.Domain.Model.Company.Holiday entity)
        {
            Id = entity.Id;
            Date = entity.Date;
            RowVersion = Convert.ToBase64String(entity.RowVersion);
        }
    }
}