using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class Tedarikci
    {
        public Tedarikci()
        {
            this.Uruns = new List<Urun>();
        }

        public int Id { get; set; }
        public string SirketAdi { get; set; }
        public string MusteriAdi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Faks { get; set; }
        public virtual ICollection<Urun> Uruns { get; set; }
    }
}
