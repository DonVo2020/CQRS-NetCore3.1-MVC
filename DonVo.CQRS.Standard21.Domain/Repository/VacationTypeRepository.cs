using DonVo.CQRS.Standard21.Domain;
using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository
{
    public class VacationTypeRepository : IVacationTypeRepository
    {
        private readonly DatabaseContext context;

        public VacationTypeRepository(DatabaseContext dbcontext)
        {
            context = dbcontext;
        }

        public async Task<VacationType> Get(int id)
        {
            return await context.VacationTypes.Include(t => t.Pool).SingleOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<VacationType>> Select()
        {
            return await context.VacationTypes.Include(t => t.Pool).ToListAsync();
        }

        public async Task<int> Insert(VacationType entity)
        {
            await context.VacationTypes.AddAsync(entity);
            if (entity.Pool != null) context.Entry(entity.Pool).State = EntityState.Unchanged;
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(VacationType entity)
        {
            if (entity.Pool != null) context.Entry(entity.Pool).State = EntityState.Unchanged;
            await context.SaveChangesAsync();
        }

        public async Task Delete(VacationType entity)
        {
            context.VacationTypes.Remove(entity);
            if (entity.Pool != null) context.Entry(entity.Pool).State = EntityState.Unchanged;
            await context.SaveChangesAsync();
        }
    }
}