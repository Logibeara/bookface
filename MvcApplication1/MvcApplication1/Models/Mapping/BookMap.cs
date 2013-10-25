using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcApplication1.Models.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            // Primary Key
            this.HasKey(t => t.BookID);

            // Properties
            this.Property(t => t.BookName)
                .IsFixedLength()
                .HasMaxLength(25);

            this.Property(t => t.ISBN)
                .IsFixedLength()
                .HasMaxLength(13);

            this.Property(t => t.Author)
                .IsFixedLength()
                .HasMaxLength(25);

            this.Property(t => t.Description)
                .IsFixedLength()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Book");
            this.Property(t => t.BookID).HasColumnName("BookID");
            this.Property(t => t.BookName).HasColumnName("BookName");
            this.Property(t => t.ISBN).HasColumnName("ISBN");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CoverPic).HasColumnName("CoverPic");
        }
    }
}
