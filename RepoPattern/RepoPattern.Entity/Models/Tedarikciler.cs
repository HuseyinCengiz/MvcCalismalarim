using System;
using System.Collections.Generic;

namespace RepoPattern.Entity.Models
{
    public partial class Tedarikciler
    {
        public Tedarikciler()
        {
            this.Urunlers = new List<Urunler>();
        }

        public int TedarikciID { get; set; }
        public string SirketAdi { get; set; }
        public string MusteriAdi { get; set; }
        public string MusteriUnvani { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Bolge { get; set; }
        public string PostaKodu { get; set; }
        public string Ulke { get; set; }
        public string Telefon { get; set; }
        public string Faks { get; set; }
        public string WebSayfasi { get; set; }
        public virtual ICollection<Urunler> Urunlers { get; set; }
    }
}
