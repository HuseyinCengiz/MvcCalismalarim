using JqueryDialogExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace JqueryDialogExample.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        public ActionResult Index(KullaniciList kullanici)
        {
            int Page = kullanici.Page ?? 1;
            JqueryAjaxDenemeContext db = new JqueryAjaxDenemeContext();
            kullanici.Kullanicis = db.Kullanicis.Where(kul =>
                                       (String.IsNullOrEmpty(kullanici.Ad) || kul.Adi.Contains(kullanici.Ad)) && (String.IsNullOrEmpty(kullanici.Soyad) || kul.Soyadi.Contains(kullanici.Soyad))).OrderByDescending(x => x.Id).ToPagedList(Page, 2);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Kullanici", kullanici);
            }
            else
            {
                return View(kullanici);
            }
        }
    }
}