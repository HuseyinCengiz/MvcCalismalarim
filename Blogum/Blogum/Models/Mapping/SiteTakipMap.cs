using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Blogum.Models.Mapping
{
    public class SiteTakipMap : EntityTypeConfiguration<SiteTakip>
    {
        public SiteTakipMap()
        {
            // Primary Key
            this.HasKey(t => t.Email);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("SiteTakip");
            this.Property(t => t.Email).HasColumnName("Email");
        }
    }
}
