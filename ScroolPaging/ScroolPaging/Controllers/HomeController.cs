using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScroolPaging.Models;
using System.Threading;

namespace ScroolPaging.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        int pageSize = 8;
        public ActionResult Index(int? page)
        {
            Thread.Sleep(3000);
            JqueryAjaxDenemeContext db = new JqueryAjaxDenemeContext();
            IEnumerable<Kullanici> kullanici = null;
            if (!page.HasValue)
            {
                kullanici = db.Kullanicis.OrderByDescending(x => x.Id).Take(pageSize).ToList();
            }
            else
            {
                int pageIndex = pageSize * page.Value;
                kullanici = db.Kullanicis.OrderByDescending(x => x.Id).Skip(pageIndex).Take(pageSize).ToList();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Kullanicilar", kullanici);
            }
            else
            {
                return View(kullanici);
            }

        }
    }
}