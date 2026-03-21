using CRUDSystem.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDSystem.Application.Abstractions
{
    public interface IDetailRepository : IDisposable
    {
        Task<List<Detail>> GetAllAsync();
        Task<Detail> GetByIdAsync(int id);
        Task AddAsync(Detail detail);
        Task UpdateAsync(Detail detail);
        Task DeleteAsync(int id);
    }
}
