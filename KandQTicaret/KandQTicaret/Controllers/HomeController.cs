using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KandQTicaret.Models;
using KandQTicaret.App_Classes;
using PagedList;
using System.Web.Security;

namespace KandQTicaret.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Slider()
        {
            return PartialView();
        }
        public PartialViewResult Markalar()
        {
            return PartialView(Context.DB.Markas.ToList());
        }
        public PartialViewResult Urunler()
        {
            return PartialView();
        }
        public PartialViewResult PopulerUrunler()
        {
            return PartialView(Context.DB.Uruns.OrderByDescending(x => x.SatisDetays.Count()).Take(8).ToList());
        }
        public PartialViewResult YeniUrunler()
        {
            return PartialView(Context.DB.Uruns.OrderByDescending(x => x.Id).Take(8).ToList());
        }
        public PartialViewResult Kategoriler()
        {
            return PartialView(Context.DB.UstKategoris.ToList());
        }
        public PartialViewResult UstMenuKategoriler()
        {
            return PartialView(Context.DB.UstKategoris.ToList());
        }
        public PartialViewResult MiniSepet()
        {
            var sepet = Sepet.AktifSepet.Urunler.ToList();
            return PartialView(sepet);
        }
        public ActionResult BuyukSepet()
        {
            ViewBag.Kargolar = Context.DB.Kargoes.ToList();
            return View(Sepet.AktifSepet.Urunler);
        }
        public void SepeteAt(int id, int Adet)
        {
            Urun urun = Context.DB.Uruns.FirstOrDefault(x => x.Id == id);
            SepetItem si = new SepetItem();
            si.Urun = urun;
            si.Adet = Adet;
            Sepet.AktifSepet.SepeteEkle(si);
        }
        public void SepettenCikar(int id)
        {
            if (Sepet.AktifSepet.Urunler.Any(x => x.Urun.Id == id))
            {
                if (Sepet.AktifSepet.Urunler.FirstOrDefault(x => x.Urun.Id == id).Adet == 1)
                {
                    Sepet.AktifSepet.Urunler.Remove(Sepet.AktifSepet.Urunler.FirstOrDefault(x => x.Urun.Id == id));
                }
                else
                {
                    Sepet.AktifSepet.Urunler.FirstOrDefault(x => x.Urun.Id == id).Adet--;
                }
            }
        }
        [HttpGet]
        public ActionResult UrunDetay(int? id)
        {
            if (id.HasValue)
            {
                Urun urun = Context.DB.Uruns.FirstOrDefault(x => x.Id == id.Value);
                ViewBag.DigerUrunler = Context.DB.Uruns.Where(x => x.KategoriID == urun.KategoriID).Take(4).ToList();
                List<UrunOzellik> uos = Context.DB.UrunOzelliks.Where(x => x.UrunID == urun.Id).ToList();
                Dictionary<string, List<OzellikDeger>> ozellikler = new Dictionary<string, List<OzellikDeger>>();
                List<OzellikDeger> degerler = new List<OzellikDeger>();
                foreach (UrunOzellik uo in uos)
                {
                    OzellikTip tip = Context.DB.OzellikTips.FirstOrDefault(x => x.Id == uo.OzellikTipID);
                    bool yenilensinmi = false;
                    foreach (var item in ozellikler)
                    {
                        if (item.Key != tip.Adi)
                            yenilensinmi = true;
                        else
                            yenilensinmi = false;
                    }
                    if (yenilensinmi)
                        degerler = new List<OzellikDeger>();
                    foreach (OzellikDeger item in tip.OzellikDegers)
                    {
                        OzellikDeger odeger = Context.DB.OzellikDegers.FirstOrDefault(x => x.Id == uo.OzellikDegerID && x.OzellikTipID == item.OzellikTipID);
                        if (!degerler.Any(x => x.Id == odeger.Id))
                            degerler.Add(odeger);
                    }
                    if (ozellikler.Any(x => x.Key == tip.Adi))
                        ozellikler[tip.Adi] = degerler;
                    else
                        ozellikler.Add(tip.Adi, degerler);
                }
                ViewBag.Ozellikler = ozellikler;
                return View(urun);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult KategoriyeGoreUrunler(int id, string SiralamaSekli, int? Page, string Renk, string Beden, int? KFiyat, int? BFiyat)
        {
            IPagedList<Urun> urunler = null;
            int SayfaNumarasi = Page ?? 1;
            int kucukfiyat = KFiyat ?? 0;
            int buyukfiyat = BFiyat ?? 20000;
            List<Urun> FiltreliUrunler = null;

            if (!String.IsNullOrEmpty(Renk) && !String.IsNullOrEmpty(Beden))
            {
                List<Urun> urunlerim = Context.DB.Uruns.Where(x => x.KategoriID == id).ToList();
                Dictionary<int, Dictionary<string, List<OzellikDeger>>> urunlistem = new Dictionary<int, Dictionary<string, List<OzellikDeger>>>();
                foreach (var urunum in urunlerim)
                {
                    List<UrunOzellik> uos = Context.DB.UrunOzelliks.Where(x => x.UrunID == urunum.Id).ToList();
                    Dictionary<string, List<OzellikDeger>> ozellikler = new Dictionary<string, List<OzellikDeger>>();
                    List<OzellikDeger> degerler = new List<OzellikDeger>();
                    foreach (UrunOzellik uo in uos)
                    {
                        OzellikTip tip = Context.DB.OzellikTips.FirstOrDefault(x => x.Id == uo.OzellikTipID);
                        if (tip.Adi == "Beden" || tip.Adi == "Renk")
                        {
                            bool yenilensinmi = false;
                            foreach (var item in ozellikler)
                            {
                                if (item.Key != tip.Adi)
                                    yenilensinmi = true;
                                else
                                    yenilensinmi = false;
                            }
                            if (yenilensinmi)
                                degerler = new List<OzellikDeger>();
                            foreach (OzellikDeger item in tip.OzellikDegers)
                            {
                                OzellikDeger odeger = Context.DB.OzellikDegers.FirstOrDefault(x => x.Id == uo.OzellikDegerID && x.OzellikTipID == item.OzellikTipID);
                                if (!degerler.Any(x => x.Id == odeger.Id))
                                    degerler.Add(odeger);
                            }
                            if (ozellikler.Any(x => x.Key == tip.Adi))
                                ozellikler[tip.Adi] = degerler;
                            else
                                ozellikler.Add(tip.Adi, degerler);
                        }
                    }
                    urunlistem.Add(urunum.Id, ozellikler);
                }
                string[] kelimeler = new string[2];
                kelimeler[0] = Beden;
                kelimeler[1] = Renk;
                foreach (var urun in urunlistem)
                {
                    int sayac = 0;
                    foreach (var ozellik in urun.Value)
                    {
                        if (ozellik.Key == "Beden")
                        {
                            foreach (OzellikDeger deger in ozellik.Value)
                            {
                                if (deger.Adi.Contains(kelimeler[0]))
                                    sayac++;
                            }
                        }
                        if (ozellik.Key == "Renk")
                        {
                            foreach (OzellikDeger deger in ozellik.Value)
                            {
                                if (deger.Adi.Contains(kelimeler[1]))
                                    sayac++;
                            }
                        }
                    }
                    if (sayac == 2)
                    {
                        if (FiltreliUrunler == null)
                            FiltreliUrunler = new List<Urun>();
                        Urun urn = Context.DB.Uruns.FirstOrDefault(x => x.Id == urun.Key);
                        FiltreliUrunler.Add(urn);
                    }
                }


            }
            else
            {
                var Urunler = (from urun in Context.DB.Uruns
                               where urun.KategoriID == id
                               join uoz in Context.DB.UrunOzelliks
                               on urun.Id equals uoz.UrunID
                               join oz in Context.DB.OzellikTips
                               on uoz.OzellikTipID equals oz.Id
                               where (String.IsNullOrEmpty(Renk) || oz.Adi.Contains("Renk")) && (String.IsNullOrEmpty(Beden) || oz.Adi.Contains("Beden"))
                               join od in Context.DB.OzellikDegers
                               on uoz.OzellikDegerID equals od.Id
                               select new
                               {
                                   Urunumuz = urun,
                                   Deger = od.Adi
                               }).ToList();

                FiltreliUrunler = (from urun in Urunler
                                   where (String.IsNullOrEmpty(Renk) || urun.Deger.Contains(Renk)) && (String.IsNullOrEmpty(Beden) || urun.Deger.Contains(Beden))
                                   select new Urun
                                   {
                                       Id = urun.Urunumuz.Id,
                                       Adi = urun.Urunumuz.Adi,
                                       Aciklama = urun.Urunumuz.Aciklama,
                                       EklenmeTarihi = urun.Urunumuz.EklenmeTarihi,
                                       Fiyat = urun.Urunumuz.Fiyat,
                                       IndirimliMi = urun.Urunumuz.IndirimliMi,
                                       IndirimOrani = urun.Urunumuz.IndirimOrani,
                                       Kategori = urun.Urunumuz.Kategori,
                                       KategoriID = urun.Urunumuz.KategoriID,
                                       Marka = urun.Urunumuz.Marka,
                                       MarkaID = urun.Urunumuz.MarkaID,
                                       Resims = urun.Urunumuz.Resims,
                                       SatisDetays = urun.Urunumuz.SatisDetays,
                                       Stok = urun.Urunumuz.Stok,
                                       Tedarikci = urun.Urunumuz.Tedarikci,
                                       TedarikciID = urun.Urunumuz.TedarikciID,
                                       UrunOzelliks = urun.Urunumuz.UrunOzelliks
                                   }).ToList();
                if (String.IsNullOrEmpty(Renk) && String.IsNullOrEmpty(Beden))
                {
                    FiltreliUrunler = (from urun in FiltreliUrunler
                                       group urun by urun.Id into gruplandı
                                       select new Urun
                                       {
                                           Id = gruplandı.Key,
                                           Adi = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).Adi,
                                           Aciklama = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).Aciklama,
                                           EklenmeTarihi = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).EklenmeTarihi,
                                           Fiyat = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).Fiyat,
                                           IndirimliMi = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).IndirimliMi,
                                           IndirimOrani = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).IndirimOrani,
                                           Kategori = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).Kategori,
                                           KategoriID = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).KategoriID,
                                           Marka = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).Marka,
                                           MarkaID = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).MarkaID,
                                           Resims = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).Resims,
                                           SatisDetays = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).SatisDetays,
                                           Stok = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).Stok,
                                           Tedarikci = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).Tedarikci,
                                           TedarikciID = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).TedarikciID,
                                           UrunOzelliks = gruplandı.FirstOrDefault(x => x.Id == gruplandı.Key).UrunOzelliks
                                       }).ToList();

                }
            }
            urunler = FiltreliUrunler.Where(x => x.Fiyat > kucukfiyat && x.Fiyat < buyukfiyat).OrderBy(X => X.EklenmeTarihi).ToPagedList(SayfaNumarasi, 9);
            ViewBag.KatId = id;
            ViewBag.siralamaSekli = SiralamaSekli;
            ViewBag.KatAdi = Context.DB.Kategoris.FirstOrDefault(x => x.Id == id).Adi;
            ViewBag.KucukFiyat = kucukfiyat;
            ViewBag.BuyukFiyat = buyukfiyat;
            ViewBag.Beden = Beden;
            ViewBag.Renk = Renk;
            if (Request.IsAjaxRequest())
            {
                if (SiralamaSekli == "price:asc")
                {
                    urunler = Context.DB.Uruns.Where(x => x.KategoriID == id && x.Fiyat > kucukfiyat && x.Fiyat < buyukfiyat).OrderBy(x => x.Fiyat).ToPagedList(SayfaNumarasi, 9);
                }
                if (SiralamaSekli == "price:desc")
                {
                    urunler = Context.DB.Uruns.Where(x => x.KategoriID == id && x.Fiyat > kucukfiyat && x.Fiyat < buyukfiyat).OrderByDescending(x => x.Fiyat).ToPagedList(SayfaNumarasi, 9);
                }
                if (SiralamaSekli == "name:asc")
                {
                    urunler = Context.DB.Uruns.Where(x => x.KategoriID == id && x.Fiyat > kucukfiyat && x.Fiyat < buyukfiyat).OrderBy(x => x.Adi).ToPagedList(SayfaNumarasi, 9);
                }
                if (SiralamaSekli == "name:desc")
                {
                    urunler = Context.DB.Uruns.Where(x => x.KategoriID == id && x.Fiyat > kucukfiyat && x.Fiyat < buyukfiyat).OrderByDescending(x => x.Adi).ToPagedList(SayfaNumarasi, 9);
                }
                return PartialView("KategoriyeGoreUrunlerWidget", urunler);
            }
            else
            {
                ViewBag.Ozellikler = Context.DB.OzellikTips.Where(x => x.KategoriID == id);
                return View(urunler);
            }
        }

        [HttpPost]
        public JsonResult SatisTamamla(IEnumerable<Siparis> siparis, decimal ToplamTutar, int KargoID)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            Guid userid = Guid.Parse(user.ProviderUserKey.ToString());
            MusteriAdre adres = Context.DB.MusteriAdres.FirstOrDefault(x => x.UserID == userid);
            jsonBilgi bilgi = new jsonBilgi();
            if (adres != null && adres.Adres != "")
            {
                Sati satis = new Sati();
                satis.SatisTarihi = DateTime.Now;
                satis.ToplamTutar = ToplamTutar;
                satis.KargoID = KargoID;
                satis.MusteriID = userid;
                SatisDurum satisdurum = Context.DB.SatisDurums.FirstOrDefault(x => x.Id == 1);
                satis.SatisDurumID = satisdurum.Id;
                satis.KargoTakipNo = "";
                Context.DB.Satis.Add(satis);
                Context.DB.SaveChanges();
                SatisDetayEkle(siparis, satis.Id);
                bilgi.IsSuccess = true;
                bilgi.Message = "Alışverişiniz Verildi. Profilinizden Alışverilerinizi Takip Edebilirsiniz.";
                return Json(bilgi, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bilgi.IsSuccess = false;
                bilgi.Message = "Alışverişinizi Vermeniz İçin Adresinizi Girmelisiniz.";
                return Json(bilgi, JsonRequestBehavior.AllowGet);
            }
        }

        public void SatisDetayEkle(IEnumerable<Siparis> siparis, int SatisID)
        {
            foreach (var urun in siparis)
            {
                SatisDetay satisdetay = new SatisDetay();
                satisdetay.UrunID = urun.UrunID;
                satisdetay.SatisID = SatisID;
                satisdetay.Adet = urun.Adet;
                satisdetay.Fiyat = urun.Fiyat;
                Context.DB.SatisDetays.Add(satisdetay);
                Context.DB.Uruns.FirstOrDefault(x => x.Id == urun.UrunID).Stok -= urun.Adet;
                Context.DB.SaveChanges();
            }
            Sepet.AktifSepet.SepetiBosalt();
        }
    }
}