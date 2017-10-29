using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JqueryDialogExample.Models;
using System.Threading;

namespace JqueryDialogExample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            Kullanici k = new Kullanici();
            return View(k);
        }
        [HttpPost]
        public JsonResult Create(Kullanici k)
        {
            JsonResultModel json = new JsonResultModel();
            try
            {
                JqueryAjaxDenemeContext db = new JqueryAjaxDenemeContext();
                Kullanici kullanici = new Kullanici();
                kullanici.Adi = k.Adi;
                kullanici.Soyadi = k.Soyadi;
                db.Kullanicis.Add(kullanici);
                db.SaveChanges();
                json.IsSuccess = true;
                json.Message = "İşlem Başarıyla Gerçekleştirildi";
            }
            catch (Exception ex)
            {
                json.IsSuccess = false;
                json.Message = "Hata" + ex;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}