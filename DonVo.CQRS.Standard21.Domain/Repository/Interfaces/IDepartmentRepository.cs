using DonVo.CQRS.Standard21.Domain.Model.Company;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> Get(int id);
        Task<IEnumerable<Department>> Select();
        Task<int> Insert(Department entity);
        Task Update(Department entity);
        Task Delete(Department entity);
    }
}