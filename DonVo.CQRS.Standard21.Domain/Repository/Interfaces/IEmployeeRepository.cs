using DonVo.CQRS.Standard21.Domain.Model.Company;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> Get(int id);
        Task<IEnumerable<Employee>> Select();
        Task<int> Insert(Employee entity);
        Task Update(Employee entity);
        Task Delete(Employee entity);
    }
}