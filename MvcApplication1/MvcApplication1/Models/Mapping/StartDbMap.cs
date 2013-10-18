using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcApplication1.Models.Mapping
{
    public class StartDbMap : EntityTypeConfiguration<StartDb>
    {
        public StartDbMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("StartDb");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.value).HasColumnName("value");
        }
    }
}
