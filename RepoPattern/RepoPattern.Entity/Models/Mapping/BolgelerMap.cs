using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace RepoPattern.Entity.Models.Mapping
{
    public class BolgelerMap : EntityTypeConfiguration<Bolgeler>
    {
        public BolgelerMap()
        {
            // Primary Key
            this.HasKey(t => t.TerritoryID);

            // Properties
            this.Property(t => t.TerritoryID)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.TerritoryTanimi)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Bolgeler");
            this.Property(t => t.TerritoryID).HasColumnName("TerritoryID");
            this.Property(t => t.TerritoryTanimi).HasColumnName("TerritoryTanimi");
            this.Property(t => t.BolgeID).HasColumnName("BolgeID");

            // Relationships
            this.HasMany(t => t.Personellers)
                .WithMany(t => t.Bolgelers)
                .Map(m =>
                    {
                        m.ToTable("PersonelBolgeler");
                        m.MapLeftKey("TerritoryID");
                        m.MapRightKey("PersonelID");
                    });

            this.HasRequired(t => t.Bolge)
                .WithMany(t => t.Bolgelers)
                .HasForeignKey(d => d.BolgeID);

        }
    }
}
