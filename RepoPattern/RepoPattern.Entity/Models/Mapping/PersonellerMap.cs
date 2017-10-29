using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace RepoPattern.Entity.Models.Mapping
{
    public class PersonellerMap : EntityTypeConfiguration<Personeller>
    {
        public PersonellerMap()
        {
            // Primary Key
            this.HasKey(t => t.PersonelID);

            // Properties
            this.Property(t => t.SoyAdi)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Adi)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Unvan)
                .HasMaxLength(30);

            this.Property(t => t.UnvanEki)
                .HasMaxLength(25);

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

            this.Property(t => t.EvTelefonu)
                .HasMaxLength(24);

            this.Property(t => t.Extension)
                .HasMaxLength(4);

            this.Property(t => t.FotografPath)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Personeller");
            this.Property(t => t.PersonelID).HasColumnName("PersonelID");
            this.Property(t => t.SoyAdi).HasColumnName("SoyAdi");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.Unvan).HasColumnName("Unvan");
            this.Property(t => t.UnvanEki).HasColumnName("UnvanEki");
            this.Property(t => t.DogumTarihi).HasColumnName("DogumTarihi");
            this.Property(t => t.IseBaslamaTarihi).HasColumnName("IseBaslamaTarihi");
            this.Property(t => t.Adres).HasColumnName("Adres");
            this.Property(t => t.Sehir).HasColumnName("Sehir");
            this.Property(t => t.Bolge).HasColumnName("Bolge");
            this.Property(t => t.PostaKodu).HasColumnName("PostaKodu");
            this.Property(t => t.Ulke).HasColumnName("Ulke");
            this.Property(t => t.EvTelefonu).HasColumnName("EvTelefonu");
            this.Property(t => t.Extension).HasColumnName("Extension");
            this.Property(t => t.Fotograf).HasColumnName("Fotograf");
            this.Property(t => t.Notlar).HasColumnName("Notlar");
            this.Property(t => t.BagliCalistigiKisi).HasColumnName("BagliCalistigiKisi");
            this.Property(t => t.FotografPath).HasColumnName("FotografPath");

            // Relationships
            this.HasOptional(t => t.Personeller2)
                .WithMany(t => t.Personeller1)
                .HasForeignKey(d => d.BagliCalistigiKisi);

        }
    }
}
