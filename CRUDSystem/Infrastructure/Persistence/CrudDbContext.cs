using CRUDSystem.Entities;
using System.Data.Entity;

namespace CRUDSystem.Infrastructure.Persistence
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext()
            : base("MyDBConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CrudDbContext, CRUDSystem.Migrations.Configuration>("MyDBConnectionString"));
        }

        public DbSet<Detail> Details { get; set; }
    }
}
