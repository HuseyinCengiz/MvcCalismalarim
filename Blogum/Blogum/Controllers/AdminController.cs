using Blogum.App_Classes;
using Blogum.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blogum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region EtiketIslemleri
        public ActionResult Etiketler()
        {
            return View(Context.Baglanti.Etikets.ToList());
        }
        public JsonResult EtiketSil(int id)
        {
            JsonResultModel json = new JsonResultModel();
            try
            {
                Etiket etkt = Context.Baglanti.Etikets.FirstOrDefault(i => i.Id == id);
                List<Makale> makalelist = new List<Makale>();
                foreach (var item in etkt.Makales)
                {
                    makalelist.Add(item);
                }
                foreach (var item in makalelist)
                {
                    etkt.Makales.Remove(item);
                }
                Context.Baglanti.Etikets.Remove(etkt);
                Context.Baglanti.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Etiket Başarıyla Silindi";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Etiket Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EtiketDuzenle(int id)
        {
            Etiket etkt = Context.Baglanti.Etikets.FirstOrDefault(x => x.Id == id);
            return View(etkt);
        }
        [HttpPost]
        public ActionResult EtiketDuzenle(Etiket etkt)
        {
            Etiket etiket = Context.Baglanti.Etikets.FirstOrDefault(x => x.Id == etkt.Id);
            etiket.EtiketAdi = etkt.EtiketAdi;
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Etiketler");
        }
        #endregion

        #region UstKategoriIslemleri
        public ActionResult UstKategoriler()
        {
            return View(Context.Baglanti.UstKategoris.ToList());
        }
        public ActionResult UstKategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UstKategoriEkle(UstKategori uk)
        {
            Context.Baglanti.UstKategoris.Add(uk);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("UstKategoriler");
        }
        public JsonResult UstKategoriSil(int id)
        {
            JsonResultModel json = new JsonResultModel();
            try
            {
                UstKategori kat = Context.Baglanti.UstKategoris.FirstOrDefault(x => x.Id == id);
                Context.Baglanti.UstKategoris.Remove(kat);
                Context.Baglanti.SaveChanges();
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
        public ActionResult UstKategoriDuzenle(int id)
        {
            UstKategori kat = Context.Baglanti.UstKategoris.FirstOrDefault(x => x.Id == id);
            return View(kat);
        }
        [HttpPost]
        public ActionResult UstKategoriDuzenle(UstKategori uk)
        {
            UstKategori k = Context.Baglanti.UstKategoris.FirstOrDefault(x => x.Id == uk.Id);
            k.UstKategoriAdi = uk.UstKategoriAdi;
            Context.Baglanti.SaveChanges();
            return RedirectToAction("UstKategoriler");
        }
        #endregion

        #region KategoriIslemleri
        public ActionResult Kategoriler()
        {
            var kat = Context.Baglanti.Kategoris.ToList();
            return View(kat);
        }
        public ActionResult KategoriEkle()
        {
            ViewBag.UstKategoriler = Context.Baglanti.UstKategoris.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            if (k.UstKategoriID > 0)
            {
                Context.Baglanti.Kategoris.Add(k);
                Context.Baglanti.SaveChanges();
                return RedirectToAction("Kategoriler");
            }
            else
            {
                ViewBag.UstKategoriler = Context.Baglanti.UstKategoris.ToList();
                ViewBag.Mesaj = "Lütfen Üst Kategori Seçiniz";
                return View();
            }
        }
        public JsonResult KategoriSil(int id)
        {
            JsonResultModel json = new JsonResultModel();
            try
            {
                Kategori kat = Context.Baglanti.Kategoris.FirstOrDefault(x => x.Id == id);
                Context.Baglanti.Kategoris.Remove(kat);
                Context.Baglanti.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Kategori Başarıyla Silindi!";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Kategori Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult KategoriDuzenle(int id)
        {
            ViewBag.UstKategoriler = Context.Baglanti.UstKategoris.ToList();
            Kategori kat = Context.Baglanti.Kategoris.FirstOrDefault(x => x.Id == id);
            return View(kat);
        }
        [HttpPost]
        public ActionResult KategoriDuzenle(Kategori k)
        {
            Kategori ka = Context.Baglanti.Kategoris.FirstOrDefault(x => x.Id == k.Id);
            if (k.UstKategoriID > 0)
            {
                ka.KategoriAdi = k.KategoriAdi;
                ka.UstKategoriID = k.UstKategoriID;
                Context.Baglanti.SaveChanges();
                return RedirectToAction("Kategoriler");
            }
            else
            {
                ViewBag.UstKategoriler = Context.Baglanti.UstKategoris.ToList();
                ViewBag.Mesaj = "Lütfen Üst Kategori Seçiniz";
                return View();
            }




        }
        #endregion

        #region MakaleIslemleri
        public ActionResult Makaleler()
        {
            return View(Context.Baglanti.Makales.ToList());
        }
        public ActionResult MakaleEkle()
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategoris.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult MakaleEkle(Makale m, HttpPostedFileBase MakaleResim, string Etiketler)
        {
            if (ModelState.IsValid)
            {
                if (m.KategoriID > 0 || MakaleResim != null)
                {
                    Makale makale = new Makale();
                    makale.Baslik = m.Baslik;
                    makale.KisaAciklama = m.KisaAciklama;
                    makale.Aciklama = m.Aciklama;
                    makale.EklenmeTarihi = DateTime.Now;
                    makale.KategoriID = m.KategoriID;
                    makale.YazarID = Guid.Parse(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
                    Context.Baglanti.Makales.Add(makale);
                    Context.Baglanti.SaveChanges();
                    int makaleid = makale.Id;
                    ResimEkle(makaleid, MakaleResim);
                    if (Etiketler != "")
                    {
                        EtiketEkle(makaleid, Etiketler);
                        return RedirectToAction("Makaleler");
                    }
                }
                else
                {
                    ViewBag.Mesaj = "Kategori ve Resim Seçiniz";
                    return RedirectToAction("MakaleEkle");
                }
            }
            else
            {
                ViewBag.Mesaj = "Gerekli Alanları Boş Bırakmayınız.";
                return RedirectToAction("MakaleEkle");
            }
            return RedirectToAction("Makaleler");
        }
        public void ResimEkle(int id, HttpPostedFileBase resim)
        {
            Resim rsm = new Resim();
            rsm.Id = id;
            Image image = Image.FromStream(resim.InputStream);
            Bitmap bmpkucuk = new Bitmap(image, MyConfig.MakaleKucukResimBoyut);
            Bitmap bmporta = new Bitmap(image, MyConfig.MakaleOrtaResimBoyut);
            Bitmap bmpbuyuk = new Bitmap(image, MyConfig.MakaleBuyukResimBoyut);
            string kucukyol = "/Content/MakaleResim/Kucuk/" + Guid.NewGuid() + Path.GetExtension(resim.FileName);
            string ortayol = "/Content/MakaleResim/Orta/" + Guid.NewGuid() + Path.GetExtension(resim.FileName);
            string buyukyol = "/Content/MakaleResim/Buyuk/" + Guid.NewGuid() + Path.GetExtension(resim.FileName);
            rsm.KucukResimYol = kucukyol;
            rsm.OrtaResimYol = ortayol;
            rsm.BuyukResimYol = buyukyol;
            bmpkucuk.Save(Server.MapPath(kucukyol));
            bmporta.Save(Server.MapPath(ortayol));
            bmpbuyuk.Save(Server.MapPath(buyukyol));
            Context.Baglanti.Resims.Add(rsm);
            Context.Baglanti.SaveChanges();
        }
        public void EtiketEkle(int makaleid, string Etiketler)
        {
            string[] etikets = Etiketler.Split(',');
            foreach (var tag in etikets)
            {
                Etiket etiket = Context.Baglanti.Etikets.FirstOrDefault(x => x.EtiketAdi.ToLower() == tag.ToLower());
                if (etiket == null)
                {
                    Etiket etkt = new Etiket();
                    etkt.EtiketAdi = tag;
                    Context.Baglanti.Etikets.Add(etkt);
                    Context.Baglanti.SaveChanges();
                }
            }
            MakaleEtiketEkle(makaleid, etikets);
        }
        public void MakaleEtiketEkle(int makaleid, string[] etiketler)
        {
            Makale mkl = Context.Baglanti.Makales.FirstOrDefault(x => x.Id == makaleid);
            foreach (var etiket in etiketler)
            {
                Etiket etkt = Context.Baglanti.Etikets.FirstOrDefault(x => x.EtiketAdi.ToLower() == etiket.ToLower());
                mkl.Etikets.Add(etkt);
            }
            Context.Baglanti.SaveChanges();
        }
        public JsonResult MakaleSil(int id)
        {
            JsonResultModel json = new JsonResultModel();
            try
            {
                Makale makale = Context.Baglanti.Makales.FirstOrDefault(x => x.Id == id);
                Resim rsm = Context.Baglanti.Resims.FirstOrDefault(x => x.Id == id);
                if (System.IO.File.Exists(Server.MapPath(rsm.KucukResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(rsm.KucukResimYol));
                }
                if (System.IO.File.Exists(Server.MapPath(rsm.OrtaResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(rsm.OrtaResimYol));
                }
                if (System.IO.File.Exists(Server.MapPath(rsm.BuyukResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(rsm.BuyukResimYol));
                }
                Context.Baglanti.Resims.Remove(rsm);
                Context.Baglanti.Makales.Remove(makale);
                Context.Baglanti.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Makale Başarıyla Silindi!";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Makale Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);

        }
        public ActionResult MakaleDuzenle(int id)
        {
            ViewBag.Kategoriler = Context.Baglanti.Kategoris.ToList();
            return View(Context.Baglanti.Makales.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        public ActionResult MakaleDuzenle(Makale m, HttpPostedFileBase MakaleResim, string Etiketler)
        {
            if (ModelState.IsValid)
            {
                Makale makale = Context.Baglanti.Makales.FirstOrDefault(x => x.Id == m.Id);
                if (m.KategoriID > 0)
                {
                    makale.Baslik = m.Baslik;
                    makale.KisaAciklama = m.KisaAciklama;
                    makale.Aciklama = m.Aciklama;
                    makale.KategoriID = m.KategoriID;
                    if (MakaleResim != null)
                    {
                        Resim rsm = Context.Baglanti.Resims.FirstOrDefault(x => x.Id == m.Id);
                        if (System.IO.File.Exists(Server.MapPath(rsm.KucukResimYol)))
                        {
                            System.IO.File.Delete(Server.MapPath(rsm.KucukResimYol));
                        }
                        if (System.IO.File.Exists(Server.MapPath(rsm.OrtaResimYol)))
                        {
                            System.IO.File.Delete(Server.MapPath(rsm.OrtaResimYol));
                        }
                        if (System.IO.File.Exists(Server.MapPath(rsm.BuyukResimYol)))
                        {
                            System.IO.File.Delete(Server.MapPath(rsm.BuyukResimYol));
                        }
                        Context.Baglanti.Resims.Remove(rsm);
                        ResimEkle(m.Id, MakaleResim);
                    }
                    string etiketler = "";
                    foreach (var etiket in makale.Etikets)
                    {
                        etiketler += etiket.EtiketAdi + ",";
                    }
                    etiketler = etiketler.Substring(0, etiketler.Length - 1);
                    if (etiketler != Etiketler)
                    {
                        List<Etiket> etikets = new List<Etiket>();
                        foreach (var item in makale.Etikets)
                        {
                            etikets.Add(item);
                        }
                        foreach (var item in etikets)
                        {
                            makale.Etikets.Remove(item);
                        }
                        Context.Baglanti.SaveChanges();
                        if (Etiketler != null || Etiketler != "")
                            EtiketEkle(m.Id, Etiketler);
                    }
                }
                else
                {
                    ViewBag.Mesaj = "Kategori Seçiniz";
                    return RedirectToAction("MakaleDuzenle", routeValues: new { id = m.Id });
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region ProfilIslemleri
        public ActionResult Profiller()
        {
            return View(Context.Baglanti.Profils.ToList());
        }
        public ActionResult ProfilEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProfilEkle(Profil profil)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            profil.UserID = Guid.Parse(user.ProviderUserKey.ToString());
            Context.Baglanti.Profils.Add(profil);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Profiller");
        }
        public ActionResult ProfilDuzenle(string id)
        {
            MembershipUser user = Membership.GetUser(id);
            Guid userid = Guid.Parse(user.ProviderUserKey.ToString());
            Profil pro = Context.Baglanti.Profils.FirstOrDefault(x => x.UserID == userid);
            return View(pro);
        }
        [HttpPost]
        public ActionResult ProfilDuzenle(Profil profil)
        {
            Profil pro = Context.Baglanti.Profils.FirstOrDefault(x => x.UserID == profil.UserID);
            pro.Biyografim = profil.Biyografim;
            pro.Twitter = profil.Twitter;
            pro.Facebook = profil.Facebook;
            pro.Google = profil.Google;
            pro.LinkedIn = profil.LinkedIn;
            Context.Baglanti.SaveChanges();
            return RedirectToAction("Profiller");
        }
        public JsonResult ProfilSil(string id)
        {
            JsonResultModel json = new JsonResultModel();
            try
            {
                MembershipUser user = Membership.GetUser(id);
                Guid userid = Guid.Parse(user.ProviderUserKey.ToString());
                Profil pro = Context.Baglanti.Profils.FirstOrDefault(x => x.UserID == userid);
                Context.Baglanti.Profils.Remove(pro);
                Context.Baglanti.SaveChanges();
                json.IsSuccess = true;
                json.Message = "Profil Başarıyla Silindi";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Profil Silinirken Hata Oluştu!";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpGet]
        public PartialViewResult IndexProfil()
        {
            MembershipUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = Membership.GetUser(User.Identity.Name);
                Guid userid = Guid.Parse(user.ProviderUserKey.ToString());
                ViewBag.KullaniciResim = Context.Baglanti.aspnet_Resim.FirstOrDefault(x => x.UserId == userid).KucukResimYol;
            }
            return PartialView(user);
        }
    }
}