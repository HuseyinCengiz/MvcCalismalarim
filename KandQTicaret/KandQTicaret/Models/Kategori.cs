using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class Kategori
    {
        public Kategori()
        {
            this.OzellikTips = new List<OzellikTip>();
            this.Uruns = new List<Urun>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public int UstKategoriID { get; set; }
        public virtual UstKategori UstKategori { get; set; }
        public virtual ICollection<OzellikTip> OzellikTips { get; set; }
        public virtual ICollection<Urun> Uruns { get; set; }
    }
}
