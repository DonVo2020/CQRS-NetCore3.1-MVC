using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly DatabaseContext context;

        public HolidayRepository(DatabaseContext dbcontext)
        {
            context = dbcontext;
        }

        public async Task<Holiday> Get(int id)
        {
            return await context.Holidays.SingleOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Holiday>> Select()
        {
            return await context.Holidays.ToListAsync();
        }

        public async Task<int> Insert(Holiday entity)
        {
            await context.Holidays.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(Holiday entity)
        {
            await context.SaveChangesAsync();
        }

        public async Task Delete(Holiday entity)
        {
            context.Holidays.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}