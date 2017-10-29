using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Blogum.Models.Mapping
{
    public class YorumMap : EntityTypeConfiguration<Yorum>
    {
        public YorumMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Icerik)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.AdSoyad)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Yorum");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Icerik).HasColumnName("Icerik");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.AdSoyad).HasColumnName("AdSoyad");
            this.Property(t => t.MakaleID).HasColumnName("MakaleID");
            this.Property(t => t.YazanID).HasColumnName("YazanID");

            // Relationships
            this.HasOptional(t => t.aspnet_Users)
                .WithMany(t => t.Yorums)
                .HasForeignKey(d => d.YazanID);
            this.HasRequired(t => t.Makale)
                .WithMany(t => t.Yorums)
                .HasForeignKey(d => d.MakaleID);

        }
    }
}
