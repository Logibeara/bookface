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

        public DbSet<Book> Books { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<StartDb> StartDbs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new ListingMap());
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new StartDbMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new webpages_MembershipMap());
            modelBuilder.Configurations.Add(new webpages_OAuthMembershipMap());
            modelBuilder.Configurations.Add(new webpages_RolesMap());
        }
    }
}
