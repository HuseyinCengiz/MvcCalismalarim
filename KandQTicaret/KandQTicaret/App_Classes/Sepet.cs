using KandQTicaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KandQTicaret.App_Classes
{
    public class Sepet
    {
        private List<SepetItem> urunler = new List<SepetItem>();
        public List<SepetItem> Urunler
        {
            get { return urunler; }
            set { urunler = value; }
        }
        public static Sepet AktifSepet
        {
            get
            {
                if (HttpContext.Current.Session["AktifSepet"] == null)
                    HttpContext.Current.Session["AktifSepet"] = new Sepet();
                return (Sepet)HttpContext.Current.Session["AktifSepet"];
            }
        }
        public decimal ToplamTutar { get { return Urunler.Sum(x => x.Tutar); } }
        public void SepeteEkle(SepetItem si)
        {
            if (!Urunler.Any(x => x.Urun.Id == si.Urun.Id))
            {
                Urunler.Add(si);
            }
            else
            {
                Urunler.FirstOrDefault(x => x.Urun.Id == si.Urun.Id).Adet += si.Adet;
            }
        }

        public void SepetiBosalt()
        {
            urunler = new List<SepetItem>();
        }
    }
    public class SepetItem
    {
        public Urun Urun { get; set; }
        public int Adet { get; set; }
        public decimal Tutar { get { return Adet * (Urun.Fiyat - ((Urun.Fiyat * Urun.IndirimOrani.Value) / 100)); } }
    }
}
