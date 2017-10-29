using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KandQTicaret.Models.Mapping
{
    public class SatisDurumMap : EntityTypeConfiguration<SatisDurum>
    {
        public SatisDurumMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Aciklama)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("SatisDurum");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Aciklama).HasColumnName("Aciklama");
        }
    }
}
