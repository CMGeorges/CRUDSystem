using CRUDSystem.Application.Abstractions;
using CRUDSystem.Application.Services;
using CRUDSystem.Infrastructure.Persistence;
using CRUDSystem.Infrastructure.Repositories;

namespace CRUDSystem.Infrastructure
{
    internal static class CompositionRoot
    {
        public static IDetailService CreateDetailService()
        {
            return new DetailService(() => new DetailRepository(new CrudDbContext()));
        }
    }
}
