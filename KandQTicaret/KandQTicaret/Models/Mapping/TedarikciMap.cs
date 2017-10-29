using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KandQTicaret.Models.Mapping
{
    public class TedarikciMap : EntityTypeConfiguration<Tedarikci>
    {
        public TedarikciMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SirketAdi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MusteriAdi)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Adres)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.Telefon)
                .IsRequired()
                .HasMaxLength(24);

            this.Property(t => t.Faks)
                .IsRequired()
                .HasMaxLength(24);

            // Table & Column Mappings
            this.ToTable("Tedarikci");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SirketAdi).HasColumnName("SirketAdi");
            this.Property(t => t.MusteriAdi).HasColumnName("MusteriAdi");
            this.Property(t => t.Adres).HasColumnName("Adres");
            this.Property(t => t.Telefon).HasColumnName("Telefon");
            this.Property(t => t.Faks).HasColumnName("Faks");
        }
    }
}
