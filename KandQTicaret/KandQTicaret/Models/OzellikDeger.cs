using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class OzellikDeger
    {
        public OzellikDeger()
        {
            this.SatisOzelliks = new List<SatisOzellik>();
            this.UrunOzelliks = new List<UrunOzellik>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public int OzellikTipID { get; set; }
        public virtual OzellikTip OzellikTip { get; set; }
        public virtual ICollection<SatisOzellik> SatisOzelliks { get; set; }
        public virtual ICollection<UrunOzellik> UrunOzelliks { get; set; }
    }
}
