using DonVo.CQRS.Standard21.Domain;
using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext context;

        public EmployeeRepository(DatabaseContext dbcontext)
        {
            context = dbcontext;
        }

        public async Task<Employee> Get(int id)
        {
            return await context.Employees
                .Include(e => e.ApplicationUser)
                .Include(e => e.Position)
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .Include(e => e.AvailableVacationDays)
                    .ThenInclude(a => a.VacationType)
                .Include(e => e.Vacations)
                    .ThenInclude(v => v.VacationType)
                        .ThenInclude(t => t.Pool)
                .Include(e => e.Contracts)
                    .ThenInclude(c => c.ContractType)
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> Select()
        {
            return await context.Employees
                .Include(e => e.ApplicationUser)
                .Include(e => e.Position)
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .Include(e => e.AvailableVacationDays)
                    .ThenInclude(a => a.VacationType)
                .Include(e => e.Vacations)
                    .ThenInclude(v => v.VacationType)
                        .ThenInclude(t => t.Pool)
                .Include(e => e.Contracts)
                    .ThenInclude(c => c.ContractType)
                .ToListAsync();
        }

        public async Task<int> Insert(Employee entity)
        {
            context.Entry(entity).State = EntityState.Added;
            context.Entry(entity.ApplicationUser).State = EntityState.Added;
            for (int i = 0; i < entity.AvailableVacationDays.Count; ++i)
                context.Entry(entity.AvailableVacationDays.ElementAt(i)).State = EntityState.Added;
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(Employee entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.Entry(entity.ApplicationUser).State = EntityState.Modified;
            for (int i = 0; i < (entity).AvailableVacationDays.Count; ++i)
                if (entity.AvailableVacationDays.ElementAt(i).Id > 0)
                    context.Entry(entity.AvailableVacationDays.ElementAt(i)).State = EntityState.Modified;
                else
                    context.Entry(entity.AvailableVacationDays.ElementAt(i)).State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        public async Task Delete(Employee entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
            context.Entry(entity.ApplicationUser).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}