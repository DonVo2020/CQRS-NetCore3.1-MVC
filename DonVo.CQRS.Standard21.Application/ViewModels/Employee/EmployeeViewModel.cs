using DonVo.CQRS.Standard21.Application.ViewModels.Department;
using DonVo.CQRS.Standard21.Application.ViewModels.Position;
using DonVo.CQRS.Standard21.Application.ViewModels.VacationType;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Employee
{
    public class EmployeeViewModel
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

        [DataType(DataType.Date), Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Required]
        public DateTimeOffset BirthDate { get; set; }

        [StringLength(32), DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }

        [StringLength(16), DataType(DataType.PhoneNumber), Required]
        public string Phone { get; set; }

        [StringLength(32), Display(Name = "Street"), Required]
        public string Street { get; set; }

        [StringLength(8), DataType(DataType.PostalCode), Display(Name = "Postal Code"), Required]
        public string PostalCode { get; set; }

        [StringLength(32), Display(Name = "City"), Required]
        public string City { get; set; }

        [Required]
        public PositionViewModel Position { get; set; }

        [Required]
        public DepartmentViewModel Department { get; set; }

        public string Manager { get; set; }

        public int? ManagerId { get; set; }

        public IList<VacationViewModel> Vacations { get; set; }

        public IList<ContractViewModel> Contracts { get; set; }

        public IList<EmployeeLiteViewModel> Subordinates { get; set; }

        public IList<VacationViewModel> SubordinateRequests { get; set; }

        public IList<StatisticViewModel> Statistics { get; set; }

        public IList<VacationTypeViewModel> VacationTypes { get; set; }

        public string RowVersion { get; set; }

        public bool IsSelected { get; set; }

        public void LoadFromDomain(DonVo.CQRS.Standard21.Domain.Model.Company.Employee entity, IEnumerable<DonVo.CQRS.Standard21.Domain.Model.Company.Holiday> holidays, int year)
        {
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
            Position = new PositionViewModel();
            Position.LoadFromDomain(entity.Position);
            Department = new DepartmentViewModel();
            Department.LoadFromDomain(entity.Department);
            Manager = entity.Manager?.FullName;
            ManagerId = entity.ManagerId;
            RowVersion = Convert.ToBase64String(entity.RowVersion);

            Vacations = new List<VacationViewModel>();
            foreach (var item in entity.GetVacations(year))
            {
                var vm = new VacationViewModel();
                vm.LoadFromDomain(item, holidays);
                Vacations.Add(vm);
            }

            Contracts = new List<ContractViewModel>();
            foreach (var item in entity.GetContracts())
            {
                var vm = new ContractViewModel();
                vm.LoadFromDomain(item);
                Contracts.Add(vm);
            }

            Statistics = new List<StatisticViewModel>();
            foreach (var item in entity.GetVacationStatistics(year, holidays))
            {
                var vm = new StatisticViewModel();
                vm.LoadFromDomain(item);
                Statistics.Add(vm);
            }
        }
    }
}