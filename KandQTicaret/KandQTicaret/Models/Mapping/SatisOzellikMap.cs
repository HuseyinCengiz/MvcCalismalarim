using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KandQTicaret.Models.Mapping
{
    public class SatisOzellikMap : EntityTypeConfiguration<SatisOzellik>
    {
        public SatisOzellikMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SatisOzellik");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.OzellikTipID).HasColumnName("OzellikTipID");
            this.Property(t => t.OzellikDegerID).HasColumnName("OzellikDegerID");
            this.Property(t => t.SatisDetayID).HasColumnName("SatisDetayID");

            // Relationships
            this.HasOptional(t => t.OzellikDeger)
                .WithMany(t => t.SatisOzelliks)
                .HasForeignKey(d => d.OzellikDegerID);
            this.HasOptional(t => t.OzellikTip)
                .WithMany(t => t.SatisOzelliks)
                .HasForeignKey(d => d.OzellikTipID);
            this.HasOptional(t => t.SatisDetay)
                .WithMany(t => t.SatisOzelliks)
                .HasForeignKey(d => d.SatisDetayID);

        }
    }
}
