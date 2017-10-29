using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KandQTicaret.Models.Mapping
{
    public class SatisDetayMap : EntityTypeConfiguration<SatisDetay>
    {
        public SatisDetayMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SatisDetay");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SatisID).HasColumnName("SatisID");
            this.Property(t => t.UrunID).HasColumnName("UrunID");
            this.Property(t => t.Adet).HasColumnName("Adet");
            this.Property(t => t.Fiyat).HasColumnName("Fiyat");

            // Relationships
            this.HasRequired(t => t.Sati)
                .WithMany(t => t.SatisDetays)
                .HasForeignKey(d => d.SatisID);
            this.HasRequired(t => t.Urun)
                .WithMany(t => t.SatisDetays)
                .HasForeignKey(d => d.UrunID);
        }
    }
}
