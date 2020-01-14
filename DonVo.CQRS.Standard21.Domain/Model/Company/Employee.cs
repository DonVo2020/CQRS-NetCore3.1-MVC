using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DonVo.CQRS.Standard21.Domain.Model.Identity;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
    public class Employee : AggregateRoot
    {
        [ForeignKey("ApplicationUser"), Required]
        public int ApplicationUserId { get; protected set; }

        [ForeignKey("ApplicationUserId"), Required]
        public ApplicationUser ApplicationUser { get; protected set; }

        [MaxLength(32), Required]
        public string FirstName { get; protected set; }

        [MaxLength(32), Required]
        public string LastName { get; protected set; }

        [Required]
        public DateTimeOffset BirthDate { get; protected set; }

        [Required]
        public bool Gender { get; protected set; }

        [NotMapped]
        public string Email => ApplicationUser?.Email;

        [NotMapped]
        public string Phone => ApplicationUser?.PhoneNumber;

        [MaxLength(32), Required]
        public string Street { get; protected set; }

        [MaxLength(8), Required]
        public string PostalCode { get; protected set; }

        [MaxLength(32), Required]
        public string City { get; protected set; }

        [ForeignKey("Position"), Required]
        public int PositionId { get; protected set; }

        [ForeignKey("PositionId"), Required]
        public Position Position { get; protected set; }

        [ForeignKey("Department"), Required]
        public int DepartmentId { get; protected set; }

        [ForeignKey("DepartmentId"), Required]
        public Department Department { get; protected set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; protected set; }

        [ForeignKey("ManagerId")]
        public Employee Manager { get; protected set; }

        public virtual ICollection<AvailableVacationDays> AvailableVacationDays { get; protected set; }

        public virtual ICollection<Vacation> Vacations { get; protected set; }

        public virtual ICollection<Contract> Contracts { get; protected set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [NotMapped]
        public Dictionary<VacationType, int> CurrentVacationDays
        {
            get => AvailableVacationDays.Any() ? AvailableVacationDays.GroupBy(v => v.VacationType).ToDictionary(k => k.Key, v => v.Any() ? v.OrderByDescending(d => d.Year).FirstOrDefault().Value : 0) : new Dictionary<VacationType, int>();
            private set
            {
                if (value != null)
                {
                    foreach (var item in value)
                    {
                        var entry = AvailableVacationDays.Where(days => days.VacationType.Equals(item.Key) && days.Year == DateTimeOffset.Now.Year).SingleOrDefault();
                        if (entry != null) entry.Update(item.Value);
                        else AvailableVacationDays.Add(new AvailableVacationDays(Id, DateTimeOffset.Now.Year, item.Key, item.Value));
                    }
                }
            }
        }

        protected Employee()
        {
            AvailableVacationDays = new List<AvailableVacationDays>();
            Vacations = new List<Vacation>();
            Contracts = new List<Contract>();
        }

        public Employee(Dictionary<VacationType, int> vacationdays, string firstname, string lastname, DateTimeOffset birthdate, bool gender, string email, string phone, string street, string postalcode, string city, Position position, Department department, Employee manager)
        {
            if (Id == manager?.Id) throw new ArgumentOutOfRangeException("Cannot be manager for yourself.");

            AvailableVacationDays = new List<AvailableVacationDays>();
            Vacations = new List<Vacation>();
            Contracts = new List<Contract>();

            CurrentVacationDays = vacationdays;
            FirstName = firstname;
            LastName = lastname;
            BirthDate = birthdate;
            Gender = gender;
            Street = street;
            PostalCode = postalcode;
            City = city;
            PositionId = position.Id;
            Position = position;
            DepartmentId = department.Id;
            Department = department;
            ManagerId = manager?.Id;
            Manager = manager;
            CreatedOn = DateTimeOffset.Now;

            if (ApplicationUser == null)
            {
                var user = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "c06ea012-a8bd-4cbe-b341-b8b2ca0acde9",
                    Email = email,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    NormalizedEmail = email.ToUpperInvariant(),
                    NormalizedUserName = email.ToUpperInvariant(),
                    PasswordHash = "AQAAAAEAACcQAAAAELHR1JlvBq+QXV7U69kooFJ2Fdliu4SSwaPNiSXnUQIjfN69QqUhfk6YWVJa0bfW5Q==", // p
                    PhoneNumber = phone,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = "ecbb9bce-d2bf-42ee-bd80-11e32338e696",
                    TwoFactorEnabled = false,
                    UserName = email
                };

                ApplicationUserId = Id;
                ApplicationUser = user;
            }
        }

        public void Update(Dictionary<VacationType, int> vacationdays, string firstname, string lastname, DateTimeOffset birthdate, bool gender, string email, string phone, string street, string postalcode, string city, Position position, Department department, Employee manager, byte[] version)
        {
            if (Id == manager?.Id) throw new ArgumentOutOfRangeException("Cannot be manager for yourself.");

            CurrentVacationDays = vacationdays;
            FirstName = firstname;
            LastName = lastname;
            BirthDate = birthdate;
            Gender = gender;
            Street = street;
            PostalCode = postalcode;
            City = city;
            PositionId = position.Id;
            Position = position;
            DepartmentId = department.Id;
            Department = department;
            ManagerId = manager?.Id;
            Manager = manager;
            ApplicationUser.Email = ApplicationUser.UserName = email;
            ApplicationUser.NormalizedEmail = ApplicationUser.NormalizedUserName = email.ToUpperInvariant();
            ApplicationUser.PhoneNumber = phone;
            RowVersion = version;
            UpdatedOn = DateTimeOffset.Now;
        }

        public void UpdateManager(Employee manager, byte[] version)
        {
            ManagerId = manager?.Id;
            Manager = manager;
            RowVersion = version;
            UpdatedOn = DateTimeOffset.Now;
        }

        public IList<VacationStatistics> GetVacationStatistics(int year, IEnumerable<Holiday> holidays)
        {
            var statistics = new List<VacationStatistics>();
            foreach (var type in CurrentVacationDays.Keys)
            {
                var currentYearDays = AvailableVacationDays.Where(days => days.VacationType.Equals(type) && days.Year <= year).OrderByDescending(days => days.Year).FirstOrDefault();
                int currentYearAvailableDays = currentYearDays != null ? currentYearDays.Value : 0;
                int previousYearOverdueDays = 0;
                var previousYearDays = AvailableVacationDays.Where(days => days.VacationType.Equals(type) && days.Year < year).OrderByDescending(days => days.Year).FirstOrDefault();
                if (type.IsPassing && previousYearDays != null)
                {
                    int previousYearAccepted = Vacations.Where(vacations => vacations.StartDate.Year == year - 1 && (vacations.VacationType.Equals(type) || vacations.VacationType.Pool != null && vacations.VacationType.Pool.Equals(type)) && vacations.Status == VacationState.Accepted).Sum(v => v.GetWorkingDaysAmount(holidays));
                    previousYearOverdueDays = (previousYearDays.Value - previousYearAccepted);
                }
                statistics.Add(new VacationStatistics
                (
                    year,
                    type,
                    currentYearAvailableDays,
                    previousYearOverdueDays,
                    (currentYearAvailableDays + previousYearOverdueDays),
                    Vacations.Where(vacations => vacations.StartDate.Year == year && vacations.VacationType.Equals(type) && vacations.Status == VacationState.Requested).Sum(v => v.GetWorkingDaysAmount(holidays)),
                    Vacations.Where(vacations => vacations.StartDate.Year == year && (vacations.VacationType.Equals(type) || (vacations.VacationType.Pool != null && vacations.VacationType.Pool.Equals(type))) && vacations.Status == VacationState.Accepted).Sum(v => v.GetWorkingDaysAmount(holidays)),
                    Vacations.Where(vacations => vacations.StartDate.Year == year && vacations.VacationType.Equals(type) && vacations.Status == VacationState.Rejected).Sum(v => v.GetWorkingDaysAmount(holidays)),
                    Vacations.Where(vacations => vacations.StartDate.Year == year && vacations.VacationType.Equals(type) && vacations.Status == VacationState.Cancelled).Sum(v => v.GetWorkingDaysAmount(holidays)),
                    Vacations.Where(vacations => vacations.StartDate.Year == year && (vacations.VacationType.Equals(type) || (vacations.VacationType.Pool != null && vacations.VacationType.Pool.Equals(type))) && vacations.Status == VacationState.Planned).Sum(v => v.GetWorkingDaysAmount(holidays))
                ));
            }
            return statistics;
        }

        public Vacation InsertVacation(DateTimeOffset start, DateTimeOffset end, VacationType type, VacationState status, IEnumerable<Holiday> holidays)
        {
            var vacation = new Vacation(Id, FullName, start, end, type, status);

            if (vacation.StartDate.CompareTo(vacation.EndDate) > 0) throw new ArgumentOutOfRangeException("Date", "Start date should be equal or greater than end date");
            if (vacation.GetWorkingDaysAmount(holidays) == 0) throw new ArgumentOutOfRangeException("Date", "Selected vacation period contains no working days");
            if (DateTimeOffset.Now.CompareTo(vacation.StartDate) > 0) throw new ArgumentOutOfRangeException("Date", "Vacation cannot be requested for the past");
            if (HasPeriodConflict(vacation)) throw new ArgumentOutOfRangeException("Date", "Vacation period is in conflict with other vacation");
            var statistics = GetVacationStatistics(DateTimeOffset.Now.Year, holidays).SingleOrDefault(s => s.Type.Equals(vacation.VacationType));
            if (statistics == null || vacation.GetWorkingDaysAmount(holidays) >= statistics.DaysLeft) throw new ArgumentOutOfRangeException("Type", "Selected vacation period exceeds available days amount");

            vacation.Request();
            Vacations.Add(vacation);
            return vacation;
        }

        public Vacation GetVacation(int id)
        {
            return Vacations.SingleOrDefault(vacation => vacation.Id == id);
        }

        public IEnumerable<Vacation> GetVacations(int year)
        {
            var result = Vacations.Where(vacation => vacation.StartDate.Year == year);
            if (result != null) return result.OrderByDescending(vacation => vacation.StartDate).ToList();
            else return new List<Vacation>();
        }

        public IEnumerable<Vacation> GetVacationRequests()
        {
            //var result = Vacations.Where(vacation => vacation.Status == VacationState.Requested && vacation.EndDate >= DateTimeOffset.Now);
            var result = Vacations.Where(vacation => vacation.Status == VacationState.Requested && (vacation.EmployeeId > 1 && vacation.EmployeeId < 4));
            if (result != null) return result.OrderBy(vacation => vacation.StartDate).ToList();
            else return new List<Vacation>();
        }

        public IEnumerable<Contract> GetContracts()
        {
            var result = Contracts;
            if (result != null) return result.OrderByDescending(contract => contract.StartDate).ToList();
            else return new List<Contract>();
        }

        public Contract InsertContract(DateTimeOffset start, DateTimeOffset? end, decimal remuneration, ContractForm contracttype)
        {
            var contract = new Contract(Id, start, end, contracttype, remuneration);
            Contracts.Add(contract);
            return contract;
        }

        public Contract DeleteContract(int id)
        {
            var contract = Contracts.Single(c => c.Id == id);
            Contracts.Remove(contract);
            return contract;
        }

        private bool HasPeriodConflict(Vacation vacation)
        {
            foreach (var item in Vacations.Where(v => (v.Status == VacationState.Accepted || v.Status == VacationState.Requested) && v.EndDate.CompareTo(DateTimeOffset.Now) >= 0))
            {
                if (vacation.StartDate.CompareTo(item.StartDate) >= 0 && vacation.StartDate.CompareTo(item.EndDate) <= 0)
                    return true;

                if (vacation.EndDate.CompareTo(item.StartDate) >= 0 && vacation.EndDate.CompareTo(item.EndDate) <= 0)
                    return true;

                if (item.StartDate.CompareTo(vacation.StartDate) >= 0 && item.EndDate.CompareTo(vacation.EndDate) <= 0)
                    return true;
            }

            return false;
        }
    }
}