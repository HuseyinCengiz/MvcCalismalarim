using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Blogum.Models.Mapping
{
    public class MakaleMap : EntityTypeConfiguration<Makale>
    {
        public MakaleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Baslik)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.KisaAciklama)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Aciklama)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Makale");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Baslik).HasColumnName("Baslik");
            this.Property(t => t.KisaAciklama).HasColumnName("KisaAciklama");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
            this.Property(t => t.BegenmeSayisi).HasColumnName("BegenmeSayisi");
            this.Property(t => t.OkunmaSayisi).HasColumnName("OkunmaSayisi");
            this.Property(t => t.EklenmeTarihi).HasColumnName("EklenmeTarihi");
            this.Property(t => t.YazarID).HasColumnName("YazarID");
            this.Property(t => t.KategoriID).HasColumnName("KategoriID");

            // Relationships
            this.HasRequired(t => t.aspnet_Users)
                .WithMany(t => t.Makales)
                .HasForeignKey(d => d.YazarID);
            this.HasRequired(t => t.Kategori)
                .WithMany(t => t.Makales)
                .HasForeignKey(d => d.KategoriID);

        }
    }
}
