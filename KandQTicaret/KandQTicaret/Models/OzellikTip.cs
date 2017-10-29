using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class OzellikTip
    {
        public OzellikTip()
        {
            this.OzellikDegers = new List<OzellikDeger>();
            this.SatisOzelliks = new List<SatisOzellik>();
            this.UrunOzelliks = new List<UrunOzellik>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public int KategoriID { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual ICollection<OzellikDeger> OzellikDegers { get; set; }
        public virtual ICollection<SatisOzellik> SatisOzelliks { get; set; }
        public virtual ICollection<UrunOzellik> UrunOzelliks { get; set; }
    }
}
