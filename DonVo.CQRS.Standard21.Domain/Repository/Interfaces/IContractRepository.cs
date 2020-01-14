using DonVo.CQRS.Standard21.Domain.Model.Company;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository.Interfaces
{
    public interface IContractRepository
    {
        Task<Contract> Get(int id);
        Task<IEnumerable<Contract>> Select();
        Task<int> Insert(Contract entity);
        Task Update(Contract entity);
        Task Delete(Contract entity);
    }
}