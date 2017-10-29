using HaberPortali.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaberPortali.Admin.Helper
{
    public static class ResimYukle
    {
        public static string Resim(HttpPostedFileBase ResimURL, Slider slider)
        {
            string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
            string[] Uzanti = ResimURL.ContentType.Split('/');
            string TamYol = "/External/Slider/" + DosyaAdi + "."+Uzanti[1];
            ResimURL.SaveAs(HttpContext.Current.Server.MapPath(TamYol));
            slider.ResimURL = TamYol;
            return slider.ResimURL;
        }
    }
}