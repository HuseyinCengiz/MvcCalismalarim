using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class SatisDetay
    {
        public SatisDetay()
        {
            this.SatisOzelliks = new List<SatisOzellik>();
        }

        public int Id { get; set; }
        public int SatisID { get; set; }
        public int UrunID { get; set; }
        public Nullable<int> Adet { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public virtual Sati Sati { get; set; }
        public virtual Urun Urun { get; set; }
        public virtual ICollection<SatisOzellik> SatisOzelliks { get; set; }
    }
}
