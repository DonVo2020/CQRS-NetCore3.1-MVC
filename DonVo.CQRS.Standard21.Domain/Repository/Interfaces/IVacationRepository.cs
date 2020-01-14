using DonVo.CQRS.Standard21.Domain.Model.Company;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository.Interfaces
{
    public interface IVacationRepository
    {
        Task<Vacation> Get(int id);
        Task<IEnumerable<Vacation>> Select();
        Task<int> Insert(Vacation entity);
        Task Update(Vacation entity);
        Task Delete(Vacation entity);
    }
}