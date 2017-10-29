using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ScroolPaging.Models.Mapping;

namespace ScroolPaging.Models
{
    public partial class JqueryAjaxDenemeContext : DbContext
    {
        static JqueryAjaxDenemeContext()
        {
            Database.SetInitializer<JqueryAjaxDenemeContext>(null);
        }

        public JqueryAjaxDenemeContext()
            : base("Name=JqueryAjaxDenemeContext")
        {
        }

        public DbSet<Kullanici> Kullanicis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new KullaniciMap());
        }
    }
}
