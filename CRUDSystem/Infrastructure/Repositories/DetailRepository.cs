using CRUDSystem.Application.Abstractions;
using CRUDSystem.Entities;
using CRUDSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CRUDSystem.Infrastructure.Repositories
{
    public class DetailRepository : IDetailRepository
    {
        private readonly CrudDbContext _context;

        public DetailRepository(CrudDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<Detail>> GetAllAsync()
        {
            return _context.Details.ToListAsync();
        }

        public Task<Detail> GetByIdAsync(int id)
        {
            return _context.Details.FirstOrDefaultAsync(detail => detail.ID == id);
        }

        public async Task AddAsync(Detail detail)
        {
            _context.Details.Add(detail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Detail detail)
        {
            _context.Entry(detail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var detail = await _context.Details.FirstOrDefaultAsync(item => item.ID == id);
            if (detail == null)
            {
                return;
            }

            _context.Details.Remove(detail);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
