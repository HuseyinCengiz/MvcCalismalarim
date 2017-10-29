using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class Sati
    {
        public Sati()
        {
            this.SatisDetays = new List<SatisDetay>();
        }

        public int Id { get; set; }
        public System.DateTime SatisTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
        public int KargoID { get; set; }
        public int SatisDurumID { get; set; }
        public System.Guid MusteriID { get; set; }
        public string KargoTakipNo { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual Kargo Kargo { get; set; }
        public virtual SatisDurum SatisDurum { get; set; }
        public virtual ICollection<SatisDetay> SatisDetays { get; set; }
    }
}
