using System;
using System.Collections.Generic;

namespace RepoPattern.Entity.Models
{
    public partial class Personeller
    {
        public Personeller()
        {
            this.Personeller1 = new List<Personeller>();
            this.Satislars = new List<Satislar>();
            this.Bolgelers = new List<Bolgeler>();
        }

        public int PersonelID { get; set; }
        public string SoyAdi { get; set; }
        public string Adi { get; set; }
        public string Unvan { get; set; }
        public string UnvanEki { get; set; }
        public Nullable<System.DateTime> DogumTarihi { get; set; }
        public Nullable<System.DateTime> IseBaslamaTarihi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Bolge { get; set; }
        public string PostaKodu { get; set; }
        public string Ulke { get; set; }
        public string EvTelefonu { get; set; }
        public string Extension { get; set; }
        public byte[] Fotograf { get; set; }
        public string Notlar { get; set; }
        public Nullable<int> BagliCalistigiKisi { get; set; }
        public string FotografPath { get; set; }
        public virtual ICollection<Personeller> Personeller1 { get; set; }
        public virtual Personeller Personeller2 { get; set; }
        public virtual ICollection<Satislar> Satislars { get; set; }
        public virtual ICollection<Bolgeler> Bolgelers { get; set; }
    }
}
