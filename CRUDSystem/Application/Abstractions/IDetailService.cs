using CRUDSystem.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDSystem.Application.Abstractions
{
    public interface IDetailService
    {
        Task<IReadOnlyList<Detail>> GetDetailsAsync();
        Task<Detail> GetDetailAsync(int id);
        Task SaveDetailAsync(Detail detail);
        Task DeleteDetailAsync(int id);
    }
}
