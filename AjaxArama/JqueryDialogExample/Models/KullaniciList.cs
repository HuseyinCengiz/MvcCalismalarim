using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace JqueryDialogExample.Models
{
    public class KullaniciList
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int? Page { get; set; }
        public IPagedList<Kullanici> Kullanicis { get; set; }
    }
}