using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Blogum.Models.Mapping
{
    public class UstKategoriMap : EntityTypeConfiguration<UstKategori>
    {
        public UstKategoriMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UstKategoriAdi)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UstKategori");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UstKategoriAdi).HasColumnName("UstKategoriAdi");
        }
    }
}
