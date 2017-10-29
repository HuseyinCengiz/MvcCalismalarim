using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Blogum.Models.Mapping
{
    public class aspnet_ResimMap : EntityTypeConfiguration<aspnet_Resim>
    {
        public aspnet_ResimMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.KucukResimYol)
                .HasMaxLength(250);

            this.Property(t => t.OrtaResimYol)
                .HasMaxLength(250);

            this.Property(t => t.BuyukResimYol)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("aspnet_Resim");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.KucukResimYol).HasColumnName("KucukResimYol");
            this.Property(t => t.OrtaResimYol).HasColumnName("OrtaResimYol");
            this.Property(t => t.BuyukResimYol).HasColumnName("BuyukResimYol");

            // Relationships
            this.HasRequired(t => t.aspnet_Users)
                .WithOptional(t => t.aspnet_Resim);

        }
    }
}
