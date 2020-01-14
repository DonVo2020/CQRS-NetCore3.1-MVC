using DonVo.CQRS.Standard21.Domain;
using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository
{
    public class VacationRepository : IVacationRepository
    {
        private readonly DatabaseContext context;

        public VacationRepository(DatabaseContext dbcontext)
        {
            context = dbcontext;
        }

        public async Task<Vacation> Get(int id)
        {
            return await context.Vacations.Include(v => v.VacationType).SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vacation>> Select()
        {
            return await context.Vacations.Include(v => v.VacationType).ToListAsync();
        }

        public async Task<int> Insert(Vacation entity)
        {
            await context.Vacations.AddAsync(entity);
            context.Entry(entity.VacationType).State = EntityState.Unchanged;
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(Vacation entity)
        {
            context.Entry(entity.VacationType).State = EntityState.Unchanged;
            await context.SaveChangesAsync();
        }

        public async Task Delete(Vacation entity)
        {
            context.Vacations.Remove(entity);
            context.Entry(entity.VacationType).State = EntityState.Unchanged;
            await context.SaveChangesAsync();
        }
    }
}