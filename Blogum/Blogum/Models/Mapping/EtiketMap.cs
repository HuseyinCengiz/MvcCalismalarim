using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Blogum.Models.Mapping
{
    public class EtiketMap : EntityTypeConfiguration<Etiket>
    {
        public EtiketMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.EtiketAdi)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Etiket");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.EtiketAdi).HasColumnName("EtiketAdi");

            // Relationships
            this.HasMany(t => t.Makales)
                .WithMany(t => t.Etikets)
                .Map(m =>
                    {
                        m.ToTable("MakaleEtiket");
                        m.MapLeftKey("EtiketID");
                        m.MapRightKey("MakaleID");
                    });


        }
    }
}
