using DonVo.CQRS.Standard21.Domain;
using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly DatabaseContext context;

        public PositionRepository(DatabaseContext dbcontext)
        {
            context = dbcontext;
        }

        public async Task<Position> Get(int id)
        {
            return await context.Positions.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Position>> Select()
        {
            return await context.Positions.ToListAsync();
        }

        public async Task<int> Insert(Position entity)
        {
            await context.Positions.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(Position entity)
        {
            await context.SaveChangesAsync();
        }

        public async Task Delete(Position entity)
        {
            context.Positions.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}