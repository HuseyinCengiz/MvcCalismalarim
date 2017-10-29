using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KandQTicaret.Models.Mapping
{
    public class KategoriMap : EntityTypeConfiguration<Kategori>
    {
        public KategoriMap()
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
            this.ToTable("Kategori");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Adi).HasColumnName("Adi");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.UstKategoriID).HasColumnName("UstKategoriID");

            // Relationships
            this.HasRequired(t => t.UstKategori)
                .WithMany(t => t.Kategoris)
                .HasForeignKey(d => d.UstKategoriID);

        }
    }
}
