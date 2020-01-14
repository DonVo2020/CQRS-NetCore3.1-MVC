using DonVo.CQRS.Standard21.Domain.Model.Company;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Domain.Repository.Interfaces
{
    public interface IPositionRepository
    {
        Task<Position> Get(int id);
        Task<IEnumerable<Position>> Select();
        Task<int> Insert(Position entity);
        Task Update(Position entity);
        Task Delete(Position entity);
    }
}