using CRUDSystem.Application.Abstractions;
using CRUDSystem.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDSystem.Application.Services
{
    public class DetailService : IDetailService
    {
        private readonly Func<IDetailRepository> _repositoryFactory;

        public DetailService(Func<IDetailRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public async Task<IReadOnlyList<Detail>> GetDetailsAsync()
        {
            using (var repository = _repositoryFactory())
            {
                return await repository.GetAllAsync();
            }
        }

        public async Task<Detail> GetDetailAsync(int id)
        {
            using (var repository = _repositoryFactory())
            {
                return await repository.GetByIdAsync(id);
            }
        }

        public async Task SaveDetailAsync(Detail detail)
        {
            if (detail == null)
            {
                throw new ArgumentNullException(nameof(detail));
            }

            using (var repository = _repositoryFactory())
            {
                if (detail.ID == 0)
                {
                    await repository.AddAsync(detail);
                    return;
                }

                await repository.UpdateAsync(detail);
            }
        }

        public async Task DeleteDetailAsync(int id)
        {
            using (var repository = _repositoryFactory())
            {
                await repository.DeleteAsync(id);
            }
        }
    }
}
