using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogum.Models
{
    public partial class UstKategori
    {
        public UstKategori()
        {
            this.Kategoris = new List<Kategori>();
        }
        [Key]
        public int Id { get; set; }
        public string UstKategoriAdi { get; set; }
        public virtual ICollection<Kategori> Kategoris { get; set; }
    }
}
