﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogum.App_Classes
{
    public class KullaniciBilgi
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Email { get; set; }
        public string GizliSoru { get; set; }
        public string GizliCevap { get; set; }
    }
}