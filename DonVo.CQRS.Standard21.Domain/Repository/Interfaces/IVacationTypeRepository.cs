using DonVo.CQRS.Standard21.Domain.Model.Company;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository.Interfaces
{
    public interface IVacationTypeRepository
    {
        Task<VacationType> Get(int id);
        Task<IEnumerable<VacationType>> Select();
        Task<int> Insert(VacationType entity);
        Task Update(VacationType entity);
        Task Delete(VacationType entity);
    }
}