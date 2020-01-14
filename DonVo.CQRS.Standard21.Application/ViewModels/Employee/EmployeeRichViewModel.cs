using DonVo.CQRS.Standard21.Application.ViewModels.Department;
using DonVo.CQRS.Standard21.Application.ViewModels.Position;
using DonVo.CQRS.Standard21.Application.ViewModels.VacationType;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Employee
{
    public class EmployeeRichViewModel
    {
        public int Id { get; set; }

        [StringLength(32), Display(Name = "First Name"), Required]
        public string FirstName { get; set; }

        [StringLength(32), Display(Name = "Last Name"), Required]
        public string LastName { get; set; }

        [StringLength(64), Display(Name = "Full Name")]
        public string FullName { get; set; }

        [StringLength(8), Display(Name = "Gender"), Required]
        public string Gender { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Birth Date"), Required]
        public DateTimeOffset BirthDate { get; set; }

        [StringLength(32), DataType(DataType.EmailAddress), Display(Name = "Email"), Required]
        public string Email { get; set; }

        [StringLength(16), DataType(DataType.PhoneNumber), Display(Name = "Phone"), Required]
        public string Phone { get; set; }

        [StringLength(32), Display(Name = "Street"), Required]
        public string Street { get; set; }

        [StringLength(8), DataType(DataType.PostalCode), Display(Name = "Postal Code"), Required]
        public string PostalCode { get; set; }

        [StringLength(32), Display(Name = "City"), Required]
        public string City { get; set; }

        [Display(Name = "Vacation Days per Year")]
        public Dictionary<string, int> AvailableVacationDays { get; set; }

        [Required, Display(Name = "Position")]
        public int PositionId { get; set; }

        [Required, Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }

        public IList<PositionViewModel> Positions { get; set; }

        public IList<DepartmentViewModel> Departments { get; set; }

        public IList<EmployeeLiteViewModel> Employees { get; set; }

        public IList<VacationTypeViewModel> VacationTypes { get; set; }

        public IList<RoleViewModel> Roles { get; set; }

        public string RowVersion { get; set; }

        public void ApplyVacationDays()
        {
            if (AvailableVacationDays != null)
            {
                foreach (var item in VacationTypes)
                {
                    item.Value = AvailableVacationDays.ContainsKey(item.Name) ? AvailableVacationDays[item.Name] : 0;
                }
            }
        }

        public void LoadFromDomain(DonVo.CQRS.Standard21.Domain.Model.Company.Employee entity)
        {
            if (entity == null)
                return;

            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            FullName = entity.FullName;
            Gender = entity.Gender ? "Male" : "Female";
            BirthDate = entity.BirthDate;
            Email = entity.Email;
            Phone = entity.Phone;
            Street = entity.Street;
            PostalCode = entity.PostalCode;
            City = entity.City;
            AvailableVacationDays = entity.CurrentVacationDays.ToDictionary(k => k.Key.Name, v => v.Value);
            PositionId = entity.Position.Id;
            DepartmentId = entity.Department.Id;
            ManagerId = entity.Manager?.Id;
            RowVersion = Convert.ToBase64String(entity.RowVersion);
        }
    }
}