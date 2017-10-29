using System;
using System.Collections.Generic;

namespace RepoPattern.Entity.Models
{
    public partial class Urunler
    {
        public Urunler()
        {
            this.SatisDetays = new List<SatisDetay>();
        }

        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public Nullable<int> TedarikciID { get; set; }
        public Nullable<int> KategoriID { get; set; }
        public string BirimdekiMiktar { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public Nullable<short> Stok { get; set; }
        public Nullable<short> YeniSatis { get; set; }
        public Nullable<short> EnAzYenidenSatisMikatari { get; set; }
        public bool Sonlandi { get; set; }
        public virtual Kategoriler Kategoriler { get; set; }
        public virtual ICollection<SatisDetay> SatisDetays { get; set; }
        public virtual Tedarikciler Tedarikciler { get; set; }
    }
}
