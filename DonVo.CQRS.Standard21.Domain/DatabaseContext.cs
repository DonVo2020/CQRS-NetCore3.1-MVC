using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Model.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DonVo.CQRS.Standard21.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Vacation> Vacations { get; set; }

        public DbSet<VacationStatus> VacationStatuses { get; set; }

        public DbSet<VacationType> VacationTypes { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractType> ContractTypes { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<AvailableVacationDays> AvailableVacationsDays { get; set; }

        public DbSet<Holiday> Holidays { get; set; }
    }
}