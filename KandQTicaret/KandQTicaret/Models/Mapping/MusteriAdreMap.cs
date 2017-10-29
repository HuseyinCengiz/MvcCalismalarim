using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KandQTicaret.Models.Mapping
{
    public class MusteriAdreMap : EntityTypeConfiguration<MusteriAdre>
    {
        public MusteriAdreMap()
        {
            // Primary Key
            this.HasKey(t => t.UserID);

            // Properties
            this.Property(t => t.Adres)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("MusteriAdres");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Adres).HasColumnName("Adres");

            // Relationships
            this.HasRequired(t => t.aspnet_Users)
                .WithOptional(t => t.MusteriAdre);

        }
    }
}
