using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class UstKategori
    {
        public UstKategori()
        {
            this.Kategoris = new List<Kategori>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public virtual ICollection<Kategori> Kategoris { get; set; }
    }
}
