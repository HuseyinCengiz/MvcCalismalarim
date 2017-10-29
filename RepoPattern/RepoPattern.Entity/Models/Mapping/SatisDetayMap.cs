using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RepoPattern.Entity.Models.Mapping
{
    public class SatisDetayMap : EntityTypeConfiguration<SatisDetay>
    {
        public SatisDetayMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SatisID, t.UrunID });

            // Properties
            this.Property(t => t.SatisID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UrunID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SatisDetay");
            this.Property(t => t.SatisID).HasColumnName("SatisID");
            this.Property(t => t.UrunID).HasColumnName("UrunID");
            this.Property(t => t.Fiyat).HasColumnName("Fiyat");
            this.Property(t => t.Adet).HasColumnName("Adet");
            this.Property(t => t.Indirim).HasColumnName("Indirim");

            // Relationships
            this.HasRequired(t => t.Satislar)
                .WithMany(t => t.SatisDetays)
                .HasForeignKey(d => d.SatisID);
            this.HasRequired(t => t.Urunler)
                .WithMany(t => t.SatisDetays)
                .HasForeignKey(d => d.UrunID);

        }
    }
}
