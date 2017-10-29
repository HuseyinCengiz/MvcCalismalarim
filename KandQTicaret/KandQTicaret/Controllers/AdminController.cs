using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KandQTicaret.Models;
using KandQTicaret.App_Classes;
using System.Drawing;
using System.IO;

namespace KandQTicaret.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        #region UstKategori Islemleri
        public ActionResult UstKategoriler()
        {
            return View(Context.DB.UstKategoris.ToList());
        }
        public ActionResult UstKategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UstKategoriEkle(UstKategori uk)
        {
            if (uk.Adi != null && uk.Adi != "")
            {
                try
                {
                    Context.DB.UstKategoris.Add(uk);
                    Context.DB.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message.ToString();
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "Lütfen Gerekli Alanları Doldurunuz.";
                return View();
            }
            return RedirectToAction("UstKategoriler", "Admin");
        }
        public JsonResult UstKategoriSil(int id)
        {
            jsonBilgi json = new jsonBilgi();
            try
            {
                UstKategori kat = Context.DB.UstKategoris.FirstOrDefault(x => x.Id == id);
                Context.DB.UstKategoris.Remove(kat);
                Context.DB.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Üst Kategori Başarıyla Silindi";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Üst Kategori Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UstKategoriDuzenle(int id)
        {
            UstKategori uk = Context.DB.UstKategoris.FirstOrDefault(x => x.Id == id);
            return View(uk);
        }
        [HttpPost]
        public ActionResult UstKategoriDuzenle(UstKategori uk)
        {
            if (ModelState.IsValid)
            {
                UstKategori kat = Context.DB.UstKategoris.FirstOrDefault(x => x.Id == uk.Id);
                kat.Adi = uk.Adi;
                Context.DB.SaveChanges();
            }
            return RedirectToAction("UstKategoriler", "Admin");
        }
        #endregion
        #region Kategori Islemleri
        public ActionResult Kategoriler()
        {
            return View(Context.DB.Kategoris.ToList());
        }
        public ActionResult KategoriEkle()
        {
            ViewBag.UstKategoriler = Context.DB.UstKategoris.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            if (k.UstKategoriID > 0)
            {
                Context.DB.Kategoris.Add(k);
                Context.DB.SaveChanges();
                return RedirectToAction("Kategoriler", "Admin");
            }
            else
            {
                ViewBag.UstKategoriler = Context.DB.UstKategoris.ToList();
                ViewBag.Mesaj = "Lütfen Üst Kategori Seçiniz";
                return View();
            }
        }
        public JsonResult KategoriSil(int id)
        {
            jsonBilgi json = new jsonBilgi();
            try
            {
                Kategori kat = Context.DB.Kategoris.FirstOrDefault(x => x.Id == id);
                Context.DB.Kategoris.Remove(kat);
                Context.DB.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Kategori Başarıyla Silindi";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Kategori Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult KategoriDuzenle(int id)
        {
            Kategori uk = Context.DB.Kategoris.FirstOrDefault(x => x.Id == id);
            ViewBag.UstKategoriler = Context.DB.UstKategoris.ToList();
            return View(uk);
        }
        [HttpPost]
        public ActionResult KategoriDuzenle(Kategori uk)
        {
            if (ModelState.IsValid)
            {
                Kategori kat = Context.DB.Kategoris.FirstOrDefault(x => x.Id == uk.Id);
                kat.Adi = uk.Adi;
                kat.Aciklama = uk.Aciklama;
                kat.UstKategoriID = uk.UstKategoriID;
                Context.DB.SaveChanges();
            }
            return RedirectToAction("Kategoriler", "Admin");
        }
        #endregion
        #region Marka Islemleri
        public ActionResult Markalar()
        {
            return View(Context.DB.Markas.ToList());
        }
        [HttpGet]
        public ActionResult MarkaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MarkaEkle(Marka m, HttpPostedFileBase MarkaResim)
        {
            Marka marka = new Marka();
            marka.Adi = m.Adi;
            marka.Aciklama = m.Aciklama;
            marka.ResimID = MarkaResimEkle(MarkaResim, Settings.MarkaResimBoyut);
            if (marka.ResimID != 0)
            {
                Context.DB.Markas.Add(marka);
                Context.DB.SaveChanges();
                return RedirectToAction("Markalar", "Admin");
            }
            else
            {
                ViewBag.Error = "Resim Yüklenirken Hata Oluştu Lütfen Tekrar Deneyiniz";
                return RedirectToAction("MarkaEkle", "Admin");
            }

        }

        private int MarkaResimEkle(HttpPostedFileBase markaResim, Size markaBoyut)
        {
            int ResimId = 0;
            Image image = Image.FromStream(markaResim.InputStream);
            Bitmap bitmapmarka = new Bitmap(image, markaBoyut);
            string ResimYol = "/Layout/MarkaResim/" + Guid.NewGuid() + Path.GetExtension(markaResim.FileName);
            Resim resim = new Resim();
            resim.KucukYol = ResimYol;
            bitmapmarka.Save(Server.MapPath(ResimYol));
            Context.DB.Resims.Add(resim);
            Context.DB.SaveChanges();
            ResimId = resim.Id;
            return ResimId;
        }
        [HttpGet]
        public ActionResult MarkaDuzenle(int id)
        {
            return View(Context.DB.Markas.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        public ActionResult MarkaDuzenle(Marka m, HttpPostedFileBase markaResim)
        {
            if (ModelState.IsValid)
            {
                Marka marka = Context.DB.Markas.FirstOrDefault(x => x.Id == m.Id);
                marka.Adi = m.Adi;
                marka.Aciklama = m.Aciklama;
                if (markaResim != null)
                {
                    MarkaResimSil(marka.ResimID.Value);
                    marka.ResimID = MarkaResimEkle(markaResim, Settings.MarkaResimBoyut);
                }
                Context.DB.SaveChanges();
            }
            return RedirectToAction("Markalar", "Admin");
        }
        public JsonResult MarkaSil(int id)
        {
            jsonBilgi json = new jsonBilgi();
            try
            {
                Marka marka = Context.DB.Markas.FirstOrDefault(x => x.Id == id);
                MarkaResimSil(marka.ResimID.Value);
                Context.DB.Markas.Remove(marka);
                Context.DB.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Marka Başarıyla Silindi";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Marka Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public void MarkaResimSil(int ResimID)
        {
            Resim resim = Context.DB.Resims.FirstOrDefault(x => x.Id == ResimID);
            if (System.IO.File.Exists(Server.MapPath(resim.KucukYol)))
            {
                System.IO.File.Delete(Server.MapPath(resim.KucukYol));
            }
            Context.DB.Resims.Remove(resim);
        }
        #endregion
        #region OzellikTip Islemleri

        public ActionResult OzellikTipleri()
        {
            return View(Context.DB.OzellikTips.ToList());
        }

        public ActionResult OzellikTipEkle()
        {
            return View(Context.DB.Kategoris.ToList());
        }
        [HttpPost]
        public ActionResult OzellikTipEkle(OzellikTip ot)
        {
            Context.DB.OzellikTips.Add(ot);
            Context.DB.SaveChanges();
            return RedirectToAction("OzellikTipleri", "Admin");
        }

        public JsonResult OzellikTipSil(int id)
        {
            jsonBilgi json = new jsonBilgi();
            try
            {
                OzellikTip ot = Context.DB.OzellikTips.FirstOrDefault(x => x.Id == id);
                List<OzellikDeger> ozellikdegerleri = Context.DB.OzellikDegers.Where(x => x.OzellikTipID == id).ToList();
                foreach (OzellikDeger item in ozellikdegerleri)
                {
                    Context.DB.OzellikDegers.Remove(item);
                }
                Context.DB.OzellikTips.Remove(ot);
                Context.DB.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Özellik Tip Başarıyla Silindi";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Özellik Tip Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region OzellikDeger Islemleri
        public ActionResult OzellikDegerleri()
        {
            return View(Context.DB.OzellikDegers.ToList());
        }
        public ActionResult OzellikDegerEkle()
        {
            return View(Context.DB.OzellikTips.ToList());
        }
        [HttpPost]
        public ActionResult OzellikDegerEkle(OzellikDeger od)
        {
            Context.DB.OzellikDegers.Add(od);
            Context.DB.SaveChanges();
            return RedirectToAction("OzellikDegerleri", "Admin");
        }
        public JsonResult OzellikDegerSil(int id)
        {
            jsonBilgi json = new jsonBilgi();
            try
            {
                OzellikDeger od = Context.DB.OzellikDegers.FirstOrDefault(x => x.Id == id);
                Context.DB.OzellikDegers.Remove(od);
                Context.DB.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Özellik Değer Başarıyla Silindi";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Özellik Değer Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Tedarikci Islemleri
        public ActionResult Tedarikciler()
        {
            return View(Context.DB.Tedarikcis.ToList());
        }
        [HttpGet]
        public ActionResult TedarikciEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TedarikciEkle(Tedarikci tdrkc)
        {
            Context.DB.Tedarikcis.Add(tdrkc);
            Context.DB.SaveChanges();
            return RedirectToAction("Tedarikciler", "Admin");
        }
        public JsonResult TedarikciSil(int id)
        {
            jsonBilgi json = new jsonBilgi();
            try
            {
                Tedarikci tdrkc = Context.DB.Tedarikcis.FirstOrDefault(x => x.Id == id);
                Context.DB.Tedarikcis.Remove(tdrkc);
                Context.DB.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Tedarikçi Başarıyla Silindi";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Tedarikçi Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Urun Islemleri
        public ActionResult Urunler()
        {
            return View(Context.DB.Uruns.ToList());
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            ViewBag.Kategoriler = Context.DB.Kategoris.ToList();
            ViewBag.Tedarikciler = Context.DB.Tedarikcis.ToList();
            ViewBag.Markalar = Context.DB.Markas.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun u, HttpPostedFileBase UrunResim)
        {
            u.EklenmeTarihi = DateTime.Now;
            Context.DB.Uruns.Add(u);
            Context.DB.SaveChanges();
            UrunresimEkle(u.Id, UrunResim);
            return RedirectToAction("Urunler", "Admin");
        }
        private void UrunresimEkle(int id, HttpPostedFileBase urunResim)
        {
            Image image = Image.FromStream(urunResim.InputStream);
            Bitmap bitmapkucukurun = new Bitmap(image, Settings.UrunResimKBoyut);
            Bitmap bitmaportaurun = new Bitmap(image, Settings.UrunResimOBoyut);
            string KucukResimYol = "/Layout/UrunResim/Kucuk/" + Guid.NewGuid() + Path.GetExtension(urunResim.FileName);
            string OrtaResimYol = "/Layout/UrunResim/Orta/" + Guid.NewGuid() + Path.GetExtension(urunResim.FileName);
            Resim resim = new Resim();
            resim.KucukYol = KucukResimYol;
            resim.OrtaYol = OrtaResimYol;
            resim.UrunID = id;
            resim.Varsayilan = true;
            bitmapkucukurun.Save(Server.MapPath(KucukResimYol));
            bitmaportaurun.Save(Server.MapPath(OrtaResimYol));
            Context.DB.Resims.Add(resim);
            Context.DB.SaveChanges();
        }
        [HttpGet]
        public ActionResult UrunResimEkle(int id)
        {
            return View(Context.DB.Uruns.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        public ActionResult UrunResimEkle(Urun urun, HttpPostedFileBase UrunResim)
        {
            Image image = Image.FromStream(UrunResim.InputStream);
            Bitmap bitmapkucukurun = new Bitmap(image, Settings.UrunResimKBoyut);
            Bitmap bitmaportaurun = new Bitmap(image, Settings.UrunResimOBoyut);
            string KucukResimYol = "/Layout/UrunResim/Kucuk/" + Guid.NewGuid() + Path.GetExtension(UrunResim.FileName);
            string OrtaResimYol = "/Layout/UrunResim/Orta/" + Guid.NewGuid() + Path.GetExtension(UrunResim.FileName);
            Resim resim = new Resim();
            resim.KucukYol = KucukResimYol;
            resim.OrtaYol = OrtaResimYol;
            resim.UrunID = urun.Id;
            List<Resim> resimler = Context.DB.Resims.Where(u => u.UrunID == urun.Id).ToList();
            if (resimler.Count > 0 && resimler != null)
            {
                foreach (Resim item in resimler)
                {
                    if (item.Varsayilan == true)
                        resim.Varsayilan = false;
                    else
                        resim.Varsayilan = true;
                }
            }
            else
            {
                resim.Varsayilan = true;
            }
            bitmapkucukurun.Save(Server.MapPath(KucukResimYol));
            bitmaportaurun.Save(Server.MapPath(OrtaResimYol));
            Context.DB.Resims.Add(resim);
            Context.DB.SaveChanges();
            return RedirectToAction("Urunler", "Admin");
        }
        public JsonResult UrunSil(int id)
        {
            jsonBilgi json = new jsonBilgi();
            try
            {
                Urun urun = Context.DB.Uruns.FirstOrDefault(x => x.Id == id);
                UrunResimSil(urun.Id);
                Context.DB.Uruns.Remove(urun);
                Context.DB.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Ürün Başarıyla Silindi";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Ürün Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        private void UrunResimSil(int id)
        {
            List<Resim> resimler = Context.DB.Resims.Where(u => u.UrunID == id).ToList();
            foreach (Resim item in resimler)
            {
                if (System.IO.File.Exists(Server.MapPath(item.KucukYol)))
                {
                    System.IO.File.Delete(Server.MapPath(item.KucukYol));
                }
                if (System.IO.File.Exists(Server.MapPath(item.OrtaYol)))
                {
                    System.IO.File.Delete(Server.MapPath(item.OrtaYol));
                }
                Context.DB.Resims.Remove(item);
            }
        }
        [HttpGet]
        public ActionResult UrunDuzenle(int id)
        {
            ViewBag.Kategoriler = Context.DB.Kategoris.ToList();
            ViewBag.Tedarikciler = Context.DB.Tedarikcis.ToList();
            ViewBag.Markalar = Context.DB.Markas.ToList();
            return View(Context.DB.Uruns.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        public ActionResult UrunDuzenle(Urun urun, HttpPostedFileBase UrunResim)
        {
            Urun urn = Context.DB.Uruns.FirstOrDefault(x => x.Id == urun.Id);
            urn.Adi = urun.Adi;
            urn.Aciklama = urun.Aciklama;
            urn.Fiyat = urun.Fiyat;
            urn.Stok = urun.Stok;
            urn.IndirimliMi = urun.IndirimliMi;
            if (urun.IndirimliMi)
            {
                urn.IndirimOrani = urun.IndirimOrani;
            }
            else
            {
                urn.IndirimOrani = 0;
            }
            urn.KategoriID = urun.KategoriID;
            urn.MarkaID = urun.MarkaID;
            urn.TedarikciID = urun.TedarikciID;
            if (UrunResim != null)
            {
                UrunResimSil(urn.Id);
                UrunresimEkle(urn.Id, UrunResim);
            }
            Context.DB.SaveChanges();
            return RedirectToAction("Urunler", "Admin");
        }
        #endregion
        #region UrunOzellikEkle
        [HttpGet]
        public ActionResult UrunOzellikler()
        {
            return View(Context.DB.UrunOzelliks.ToList());
        }

        [HttpGet]
        public ActionResult UrunOzellikEkle()
        {
            return View(Context.DB.Uruns.ToList());
        }
        [HttpPost]
        public ActionResult UrunOzellikEkle(UrunOzellik uoz)
        {
            if (uoz.OzellikDegerID > 0 && uoz.OzellikTipID > 0 && uoz.UrunID > 0)
            {
                Context.DB.UrunOzelliks.Add(uoz);
                Context.DB.SaveChanges();
                return RedirectToAction("UrunOzellikler", "Admin");
            }
            else
            {
                ViewBag.Error = "Lütfen Bütün Alanları Doldurunuz.";
                return View(Context.DB.Uruns.ToList());
            }

        }

        [HttpGet]
        public PartialViewResult OzellikTipWidget(int? katID)
        {
            if (katID == null)
            {
                List<OzellikTip> OzellikTip = Context.DB.OzellikTips.ToList();
                return PartialView(OzellikTip);
            }
            else
            {
                List<OzellikTip> OzellikTip = Context.DB.OzellikTips.Where(x => x.KategoriID == katID).ToList();
                return PartialView(OzellikTip);
            }
        }

        [HttpGet]
        public PartialViewResult OzellikDegerWidget(int? OTipID)
        {
            if (OTipID == null)
            {
                List<OzellikDeger> OzellikDeger = Context.DB.OzellikDegers.ToList();
                return PartialView(OzellikDeger);
            }
            else
            {
                List<OzellikDeger> OzellikDeger = Context.DB.OzellikDegers.Where(x => x.OzellikTipID == OTipID).ToList();
                return PartialView(OzellikDeger);
            }
        }

        public ActionResult UrunOzellikSil(int? UrunId, int? OzellikTipId, int? OzellikDegerId)
        {
            if (UrunId.HasValue && OzellikTipId.HasValue && OzellikDegerId.HasValue)
            {
                UrunOzellik uoz = Context.DB.UrunOzelliks.FirstOrDefault(x => x.UrunID == UrunId && x.OzellikTipID == OzellikTipId && x.OzellikDegerID == OzellikDegerId);
                Context.DB.UrunOzelliks.Remove(uoz);
                Context.DB.SaveChanges();
            }
            return RedirectToAction("UrunOzellikler", "Admin");
        }
        #endregion
    }
}