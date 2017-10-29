using System;
using System.Collections.Generic;

namespace RepoPattern.Entity.Models
{
    public partial class Musteriler
    {
        public Musteriler()
        {
            this.Satislars = new List<Satislar>();
            this.MusteriDemographics = new List<MusteriDemographic>();
        }

        public string MusteriID { get; set; }
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
        public virtual ICollection<Satislar> Satislars { get; set; }
        public virtual ICollection<MusteriDemographic> MusteriDemographics { get; set; }
    }
}
