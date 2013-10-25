using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcApplication1.Models.Mapping
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            // Primary Key
            this.HasKey(t => t.CourseID);

            // Properties
            this.Property(t => t.Dept)
                .HasMaxLength(50);

            this.Property(t => t.CourseName)
                .HasMaxLength(25);

            this.Property(t => t.Section)
                .HasMaxLength(50);

            this.Property(t => t.Semester)
                .HasMaxLength(8);

            this.Property(t => t.Instructor)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Course");
            this.Property(t => t.CourseID).HasColumnName("CourseID");
            this.Property(t => t.Dept).HasColumnName("Dept");
            this.Property(t => t.CourseNumber).HasColumnName("CourseNumber");
            this.Property(t => t.CourseName).HasColumnName("CourseName");
            this.Property(t => t.Section).HasColumnName("Section");
            this.Property(t => t.Semester).HasColumnName("Semester");
            this.Property(t => t.Instructor).HasColumnName("Instructor");
        }
    }
}
