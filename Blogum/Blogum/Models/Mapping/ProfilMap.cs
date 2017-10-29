using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Blogum.Models.Mapping
{
    public class ProfilMap : EntityTypeConfiguration<Profil>
    {
        public ProfilMap()
        {
            // Primary Key
            this.HasKey(t => t.UserID);

            // Properties
            this.Property(t => t.Biyografim)
                .IsRequired()
                .HasMaxLength(2000);

            this.Property(t => t.Twitter)
                .HasMaxLength(250);

            this.Property(t => t.Facebook)
                .HasMaxLength(250);

            this.Property(t => t.Google)
                .HasMaxLength(250);

            this.Property(t => t.LinkedIn)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Profil");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Biyografim).HasColumnName("Biyografim");
            this.Property(t => t.Twitter).HasColumnName("Twitter");
            this.Property(t => t.Facebook).HasColumnName("Facebook");
            this.Property(t => t.Google).HasColumnName("Google");
            this.Property(t => t.LinkedIn).HasColumnName("LinkedIn");

            // Relationships
            this.HasRequired(t => t.aspnet_Users)
                .WithOptional(t => t.Profil);

        }
    }
}
