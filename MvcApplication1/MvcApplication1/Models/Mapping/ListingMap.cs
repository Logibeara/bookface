using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcApplication1.Models.Mapping
{
    public class ListingMap : EntityTypeConfiguration<Listing>
    {
        public ListingMap()
        {
            // Primary Key
            this.HasKey(t => t.ListID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Listing");
            this.Property(t => t.ListID).HasColumnName("ListID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.BookID).HasColumnName("BookID");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.ListType).HasColumnName("ListType");
            this.Property(t => t.ListDate).HasColumnName("ListDate");
        }
    }
}
