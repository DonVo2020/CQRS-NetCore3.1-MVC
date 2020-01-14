using DonVo.CQRS.Standard21.Domain;
using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DatabaseContext context;

        public DepartmentRepository(DatabaseContext dbcontext)
        {
            context = dbcontext;
        }

        public async Task<Department> Get(int id)
        {
            return await context.Departments.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Department>> Select()
        {
            return await context.Departments.ToListAsync();
        }

        public async Task<int> Insert(Department entity)
        {
            await context.Departments.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(Department entity)
        {
            await context.SaveChangesAsync();
        }

        public async Task Delete(Department entity)
        {
            context.Departments.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}