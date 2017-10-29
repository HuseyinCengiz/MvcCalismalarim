using HaberPortali.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberPortali.Data.DataContext
{
    public class HaberContext : DbContext
    {
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Haber> Haber { get; set; }
        public DbSet<Resim> Resim { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Etiket> Etiket { get; set; }
    }
}
