using Blogum.App_Classes;
using Blogum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Security;
using System.Net.Mail;
using System.Text;

namespace Blogum.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int? page)
        {
            page = page ?? 1;
            IPagedList<Makale> makaleler = Context.Baglanti.Makales.OrderByDescending(x => x.Id).ToPagedList<Makale>(page.Value, 5);
            return View(makaleler);
        }
        public ActionResult MakaleDetay(int id)
        {
            ViewBag.SayfaBilgi = "MakaleDetay";
            Okunma(id);
            Makale makale = Context.Baglanti.Makales.FirstOrDefault(x => x.Id == id);
            ViewBag.MakaleBilgi = makale;
            List<Makale> makalelist = Context.Baglanti.Makales.Where(x => x.KategoriID == makale.KategoriID && x.Id != makale.Id).Take(4).ToList();
            if (makalelist.Count() > 0)
            {
                ViewBag.iliskilimakaleler = makalelist;
            }
            return View(makale);
        }
        public PartialViewResult SonMakaleler()
        {
            ViewBag.PopiMakaleler = Context.Baglanti.Makales.OrderByDescending(x => x.OkunmaSayisi).Take(5).ToList();
            ViewBag.SonMakaleler = Context.Baglanti.Makales.OrderByDescending(x => x.Id).Take(5).ToList();
            ViewBag.YorumMakaleler = Context.Baglanti.Makales.OrderByDescending(x => x.Yorums.Count()).Take(5).ToList();
            return PartialView();
        }
        public PartialViewResult Kategoriler()
        {
            ViewBag.Kategoriler = Context.Baglanti.UstKategoris.ToList();
            return PartialView();
        }
        public ActionResult Kategori(int id, int page = 1)
        {
            ViewBag.kategori = Context.Baglanti.Kategoris.FirstOrDefault(x => x.Id == id);
            var makale = Context.Baglanti.Makales.Where(x => x.KategoriID == id).OrderByDescending(x => x.Id).ToPagedList(page, 5);
            return View(makale);
        }

        public ActionResult Aramalar(string id, int? page)
        {
            page = page ?? 1;
            if (id != "")
            {
                ViewBag.aranan = id;
                var makale = Context.Baglanti.Makales.Where(x => x.Baslik.Contains(id)).OrderByDescending(x => x.Id).ToPagedList(page.Value, 5);
                return View(makale);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public PartialViewResult Etiketler()
        {
            var etiketler = Context.Baglanti.Etikets.ToList();
            return PartialView(etiketler);
        }
        [HttpPost]
        public void YorumEkle(Yorum yorum, string YazanKisi)
        {
            if (YazanKisi != "")
            {
                MembershipUser user = Membership.GetUser(YazanKisi);
                yorum.YazanID = Guid.Parse(user.ProviderUserKey.ToString());
            }
            Context.Baglanti.Yorums.Add(yorum);
            Context.Baglanti.SaveChanges();
        }
        public PartialViewResult Profil()
        {
            MembershipUser user = null;
            Blogum.Models.Profil pro = null;
            string[] users = Roles.GetUsersInRole("Admin");
            foreach (var item in users)
            {
                if (item == "huseyin57")
                {
                    user = Membership.GetUser(item);

                }
            }
            if (user != null)
            {
                Guid userid = Guid.Parse(user.ProviderUserKey.ToString());
                pro = Context.Baglanti.Profils.FirstOrDefault(x => x.UserID == userid);
            }
            return PartialView(pro);
        }
        public JsonResult Search(string term)
        {
            List<Makale> makaleler = Context.Baglanti.Makales.Where(x => x.Baslik.StartsWith(term)).ToList();
            var makale = makaleler.Select(x => new { id = x.Id, value = x.Baslik, url = "/Home/Aramalar/" + x.Baslik });
            return Json(makale, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Iletisim(Iletisim i)
        {
            JsonResultModel json = new JsonResultModel();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("");
                mail.To.Add(new MailAddress(""));
                mail.Subject = i.Konu;
                mail.Body = i.Email + "Email Adresinden " + i.AdSoyad + "Adlı Kişiden Mesaj= " + i.Mesaj;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(mail);
                json.IsSuccess = true;
                json.Message = "Mesajınız Başarıyla Gönderilmiştir.";
            }
            catch (Exception)
            {
                json.IsSuccess = false;
                json.Message = "Mail Gönderirken Hata Oluştu Lütfen Daha Sonra Deneyiniz";
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public void Okunma(int id)
        {
            Makale m = Context.Baglanti.Makales.FirstOrDefault(x => x.Id == id);
            m.OkunmaSayisi += 1;
            Context.Baglanti.SaveChanges();
        }
        [HttpPost]
        public JsonResult Begenme(int id, bool begendimi)
        {
            Makale m = Context.Baglanti.Makales.FirstOrDefault(x => x.Id == id);
            string sonuc = "";
            if (begendimi == true)
            {
                m.BegenmeSayisi += 1;
                StringBuilder s = new StringBuilder();
                s.Append("<i class='fa fa-heart'></i><span id='begenmesayisi'>");
                s.Append("(" + m.BegenmeSayisi + ")</span>");
                sonuc = s.ToString();
            }
            else
            {
                m.BegenmeSayisi -= 1;
                StringBuilder s = new StringBuilder();
                s.Append("<i class='fa fa-heart-o'></i><span id='begenmesayisi'>");
                s.Append("(" + m.BegenmeSayisi + ")</span>");
                sonuc = s.ToString();
            }
            Context.Baglanti.SaveChanges();
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
    }
}