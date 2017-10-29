using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace RepoPattern.Entity.Models.Mapping
{
    public class MusteriDemographicMap : EntityTypeConfiguration<MusteriDemographic>
    {
        public MusteriDemographicMap()
        {
            // Primary Key
            this.HasKey(t => t.MusteriTypeID);

            // Properties
            this.Property(t => t.MusteriTypeID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("MusteriDemographics");
            this.Property(t => t.MusteriTypeID).HasColumnName("MusteriTypeID");
            this.Property(t => t.MusteriDesc).HasColumnName("MusteriDesc");

            // Relationships
            this.HasMany(t => t.Musterilers)
                .WithMany(t => t.MusteriDemographics)
                .Map(m =>
                    {
                        m.ToTable("MusteriMusteriDemo");
                        m.MapLeftKey("MusteriTypeID");
                        m.MapRightKey("MusteriID");
                    });


        }
    }
}
