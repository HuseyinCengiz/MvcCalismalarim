using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KandQTicaret.Models.Mapping
{
    public class SatiMap : EntityTypeConfiguration<Sati>
    {
        public SatiMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.KargoTakipNo)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Satis");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SatisTarihi).HasColumnName("SatisTarihi");
            this.Property(t => t.ToplamTutar).HasColumnName("ToplamTutar");
            this.Property(t => t.KargoID).HasColumnName("KargoID");
            this.Property(t => t.SatisDurumID).HasColumnName("SatisDurumID");
            this.Property(t => t.MusteriID).HasColumnName("MusteriID");
            this.Property(t => t.KargoTakipNo).HasColumnName("KargoTakipNo");

            // Relationships
            this.HasRequired(t => t.aspnet_Users)
                .WithMany(t => t.Satis)
                .HasForeignKey(d => d.MusteriID);
            this.HasRequired(t => t.Kargo)
                .WithMany(t => t.Satis)
                .HasForeignKey(d => d.KargoID);
            this.HasRequired(t => t.SatisDurum)
                .WithMany(t => t.Satis)
                .HasForeignKey(d => d.SatisDurumID);

        }
    }
}
