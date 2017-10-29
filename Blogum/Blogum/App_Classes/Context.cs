using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogum.App_Classes
{
    public static class Context
    {
        private static Blogum.Models.BlogumContext baglanti;
        public static Blogum.Models.BlogumContext Baglanti
        {
            get
            {
                if (baglanti == null)
                    baglanti = new Models.BlogumContext();
                return baglanti;
            }
        }
    }
}