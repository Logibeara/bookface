using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MvcApplication1.Models.Mapping;

namespace MvcApplication1.Models
{
    public partial class TestDbContext : DbContext
    {
        static TestDbContext()
        {
            Database.SetInitializer<TestDbContext>(null);
        }

        public TestDbContext()
            : base("Name=TestDbContext")
        {
        }

        public DbSet<StartDb> StartDbs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StartDbMap());
        }
    }
}
