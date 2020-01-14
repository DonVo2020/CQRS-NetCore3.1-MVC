using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly DatabaseContext context;

        public ContractRepository(DatabaseContext dbcontext)
        {
            context = dbcontext;
        }

        public async Task<Contract> Get(int id)
        {
            return await context.Contracts.SingleOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Contract>> Select()
        {
            return await context.Contracts.ToListAsync();
        }

        public async Task<int> Insert(Contract entity)
        {
            await context.Contracts.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(Contract entity)
        {
            await context.SaveChangesAsync();
        }

        public async Task Delete(Contract entity)
        {
            context.Contracts.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}