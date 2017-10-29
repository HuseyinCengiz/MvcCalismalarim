using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogum.Models
{
    public partial class Kategori
    {
        public Kategori()
        {
            this.Makales = new List<Makale>();
        }
        [Key]
        public int Id { get; set; }
        public string KategoriAdi { get; set; }
        public int UstKategoriID { get; set; }
        public virtual UstKategori UstKategori { get; set; }
        public virtual ICollection<Makale> Makales { get; set; }
    }
}
