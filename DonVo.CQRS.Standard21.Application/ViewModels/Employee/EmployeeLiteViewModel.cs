using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Employee
{
    public class EmployeeLiteViewModel
    {
        public int Id { get; set; }

        [StringLength(32), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(32), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(64), Display(Name = "Full Name")]
        public string FullName { get; set; }

        [StringLength(8), Display(Name = "Gender")]
        public string Gender { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Birth Date")]
        public DateTimeOffset BirthDate { get; set; }

        [StringLength(32), DataType(DataType.EmailAddress), Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(16), DataType(DataType.PhoneNumber), Display(Name = "Phone")]
        public string Phone { get; set; }

        [StringLength(32), Display(Name = "Position")]
        public string Position { get; set; }

        [StringLength(32), Display(Name = "Department")]
        public string Department { get; set; }

        public int? ManagerId { get; set; }

        public bool IsSelected { get; set; }

        public void LoadFromDomain(DonVo.CQRS.Standard21.Domain.Model.Company.Employee entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            FullName = entity.FullName;
            Gender = entity.Gender ? "Male" : "Female";
            BirthDate = entity.BirthDate;
            Email = entity.Email;
            Phone = entity.Phone;
            Position = entity.Position.Name;
            Department = entity.Department.Name;
            ManagerId = entity.ManagerId;
        }
    }
}