using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace RepoPattern.Entity.Models.Mapping
{
    public class SatislarMap : EntityTypeConfiguration<Satislar>
    {
        public SatislarMap()
        {
            // Primary Key
            this.HasKey(t => t.SatisID);

            // Properties
            this.Property(t => t.MusteriID)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.SevkAdi)
                .HasMaxLength(40);

            this.Property(t => t.SevkAdresi)
                .HasMaxLength(60);

            this.Property(t => t.SevkSehri)
                .HasMaxLength(15);

            this.Property(t => t.SevkBolgesi)
                .HasMaxLength(15);

            this.Property(t => t.SevkPostaKodu)
                .HasMaxLength(10);

            this.Property(t => t.SevkUlkesi)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("Satislar");
            this.Property(t => t.SatisID).HasColumnName("SatisID");
            this.Property(t => t.MusteriID).HasColumnName("MusteriID");
            this.Property(t => t.PersonelID).HasColumnName("PersonelID");
            this.Property(t => t.SatisTarihi).HasColumnName("SatisTarihi");
            this.Property(t => t.OdemeTarihi).HasColumnName("OdemeTarihi");
            this.Property(t => t.SevkTarihi).HasColumnName("SevkTarihi");
            this.Property(t => t.ShipVia).HasColumnName("ShipVia");
            this.Property(t => t.NakliyeUcreti).HasColumnName("NakliyeUcreti");
            this.Property(t => t.SevkAdi).HasColumnName("SevkAdi");
            this.Property(t => t.SevkAdresi).HasColumnName("SevkAdresi");
            this.Property(t => t.SevkSehri).HasColumnName("SevkSehri");
            this.Property(t => t.SevkBolgesi).HasColumnName("SevkBolgesi");
            this.Property(t => t.SevkPostaKodu).HasColumnName("SevkPostaKodu");
            this.Property(t => t.SevkUlkesi).HasColumnName("SevkUlkesi");

            // Relationships
            this.HasOptional(t => t.Musteriler)
                .WithMany(t => t.Satislars)
                .HasForeignKey(d => d.MusteriID);
            this.HasOptional(t => t.Nakliyeciler)
                .WithMany(t => t.Satislars)
                .HasForeignKey(d => d.ShipVia);
            this.HasOptional(t => t.Personeller)
                .WithMany(t => t.Satislars)
                .HasForeignKey(d => d.PersonelID);

        }
    }
}
