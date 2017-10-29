using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace RepoPattern.Entity.Models.Mapping
{
    public class UrunlerMap : EntityTypeConfiguration<Urunler>
    {
        public UrunlerMap()
        {
            // Primary Key
            this.HasKey(t => t.UrunID);

            // Properties
            this.Property(t => t.UrunAdi)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.BirimdekiMiktar)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Urunler");
            this.Property(t => t.UrunID).HasColumnName("UrunID");
            this.Property(t => t.UrunAdi).HasColumnName("UrunAdi");
            this.Property(t => t.TedarikciID).HasColumnName("TedarikciID");
            this.Property(t => t.KategoriID).HasColumnName("KategoriID");
            this.Property(t => t.BirimdekiMiktar).HasColumnName("BirimdekiMiktar");
            this.Property(t => t.Fiyat).HasColumnName("Fiyat");
            this.Property(t => t.Stok).HasColumnName("Stok");
            this.Property(t => t.YeniSatis).HasColumnName("YeniSatis");
            this.Property(t => t.EnAzYenidenSatisMikatari).HasColumnName("EnAzYenidenSatisMikatari");
            this.Property(t => t.Sonlandi).HasColumnName("Sonlandi");

            // Relationships
            this.HasOptional(t => t.Kategoriler)
                .WithMany(t => t.Urunlers)
                .HasForeignKey(d => d.KategoriID);
            this.HasOptional(t => t.Tedarikciler)
                .WithMany(t => t.Urunlers)
                .HasForeignKey(d => d.TedarikciID);

        }
    }
}
