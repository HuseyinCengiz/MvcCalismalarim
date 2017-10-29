using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KandQTicaret.Models.Mapping
{
    public class UrunMap : EntityTypeConfiguration<Urun>
    {
        public UrunMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Adi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Aciklama)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Urun");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.Fiyat).HasColumnName("Fiyat");
            this.Property(t => t.Stok).HasColumnName("Stok");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");
            this.Property(t => t.IndirimliMi).HasColumnName("IndirimliMi");
            this.Property(t => t.IndirimOrani).HasColumnName("IndirimOrani");
            this.Property(t => t.KategoriID).HasColumnName("KategoriID");
            this.Property(t => t.MarkaID).HasColumnName("MarkaID");
            this.Property(t => t.TedarikciID).HasColumnName("TedarikciID");

            // Relationships
            this.HasRequired(t => t.Kategori)
                .WithMany(t => t.Uruns)
                .HasForeignKey(d => d.KategoriID);
            this.HasRequired(t => t.Marka)
                .WithMany(t => t.Uruns)
                .HasForeignKey(d => d.MarkaID);
            this.HasRequired(t => t.Tedarikci)
                .WithMany(t => t.Uruns)
                .HasForeignKey(d => d.TedarikciID);

        }
    }
}
