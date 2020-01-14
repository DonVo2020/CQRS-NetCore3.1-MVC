using DonVo.CQRS.Standard21.Domain.Model.Company;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository.Interfaces
{
    public interface IHolidayRepository
    {
        Task<Holiday> Get(int id);
        Task<IEnumerable<Holiday>> Select();
        Task<int> Insert(Holiday entity);
        Task Update(Holiday entity);
        Task Delete(Holiday entity);
    }
}