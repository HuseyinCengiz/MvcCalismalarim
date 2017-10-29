using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Blogum.Models.Mapping
{
    public class ResimMap : EntityTypeConfiguration<Resim>
    {
        public ResimMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.KucukResimYol)
                .HasMaxLength(250);

            this.Property(t => t.OrtaResimYol)
                .HasMaxLength(250);

            this.Property(t => t.BuyukResimYol)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Resim");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.KucukResimYol).HasColumnName("KucukResimYol");
            this.Property(t => t.OrtaResimYol).HasColumnName("OrtaResimYol");
            this.Property(t => t.BuyukResimYol).HasColumnName("BuyukResimYol");

            // Relationships
            this.HasRequired(t => t.Makale)
                .WithOptional(t => t.Resim);

        }
    }
}
