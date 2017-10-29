using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HaberPortali.Admin.CustomFilter
{
    public class LoginFilter : FilterAttribute, IActionFilter
    {
        //Action metod çalıştırıldıktan sonra çalışır
        public void OnActionExecuted(ActionExecutedContext context)
        {
            HttpContextWrapper wrapper = new HttpContextWrapper(HttpContext.Current);//geçerli httpcontexti alıyor
            var SessionControl = context.HttpContext.Session["KullaniciEmail"];//sessionı aldık
            if (SessionControl == null)//eğer session boş ise routing yapıyoruz.
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
                //sonuca yeni yönlendirme route veriyoruz bunada route value dictionary ile yapıyoruz.
            }
        }

        //Action metod  tetiklediğinde çalışır
        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}