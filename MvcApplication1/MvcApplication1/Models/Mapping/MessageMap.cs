using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcApplication1.Models.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.MessageID);

            // Properties
            this.Property(t => t.Message1)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Message");
            this.Property(t => t.MessageID).HasColumnName("MessageID");
            this.Property(t => t.RecipientID).HasColumnName("RecipientID");
            this.Property(t => t.SenderID).HasColumnName("SenderID");
            this.Property(t => t.Message1).HasColumnName("Message");
            this.Property(t => t.IsRead).HasColumnName("IsRead");
            this.Property(t => t.SendDate).HasColumnName("SendDate");
            this.Property(t => t.SenderName).HasColumnName("SenderName");
        }
    }
}
