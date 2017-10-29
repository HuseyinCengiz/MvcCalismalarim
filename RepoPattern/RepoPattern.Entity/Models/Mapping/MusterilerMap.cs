using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace RepoPattern.Entity.Models.Mapping
{
    public class MusterilerMap : EntityTypeConfiguration<Musteriler>
    {
        public MusterilerMap()
        {
            // Primary Key
            this.HasKey(t => t.MusteriID);

            // Properties
            this.Property(t => t.MusteriID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.SirketAdi)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.MusteriAdi)
                .HasMaxLength(30);

            this.Property(t => t.MusteriUnvani)
                .HasMaxLength(30);

            this.Property(t => t.Adres)
                .HasMaxLength(60);

            this.Property(t => t.Sehir)
                .HasMaxLength(15);

            this.Property(t => t.Bolge)
                .HasMaxLength(15);

            this.Property(t => t.PostaKodu)
                .HasMaxLength(10);

            this.Property(t => t.Ulke)
                .HasMaxLength(15);

            this.Property(t => t.Telefon)
                .HasMaxLength(24);

            this.Property(t => t.Faks)
                .HasMaxLength(24);

            // Table & Column Mappings
            this.ToTable("Musteriler");
            this.Property(t => t.MusteriID).HasColumnName("MusteriID");
            this.Property(t => t.SirketAdi).HasColumnName("SirketAdi");
            this.Property(t => t.MusteriAdi).HasColumnName("MusteriAdi");
            this.Property(t => t.MusteriUnvani).HasColumnName("MusteriUnvani");
            this.Property(t => t.Adres).HasColumnName("Adres");
            this.Property(t => t.Sehir).HasColumnName("Sehir");
            this.Property(t => t.Bolge).HasColumnName("Bolge");
            this.Property(t => t.PostaKodu).HasColumnName("PostaKodu");
            this.Property(t => t.Ulke).HasColumnName("Ulke");
            this.Property(t => t.Telefon).HasColumnName("Telefon");
            this.Property(t => t.Faks).HasColumnName("Faks");
        }
    }
}
