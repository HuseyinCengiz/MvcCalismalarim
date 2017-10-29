using System;
using System.Collections.Generic;

namespace RepoPattern.Entity.Models
{
    public partial class SatisDetay
    {
        public int SatisID { get; set; }
        public int UrunID { get; set; }
        public decimal Fiyat { get; set; }
        public short Adet { get; set; }
        public float Indirim { get; set; }
        public virtual Satislar Satislar { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}
