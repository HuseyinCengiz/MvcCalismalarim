using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace KandQTicaret.App_Classes
{
    public static class Settings
    {
        public static Size MarkaResimBoyut
        {
            get
            {
                return new Size { Width = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaResimW"].ToString()), Height = Convert.ToInt32(ConfigurationManager.AppSettings["MarkaResimH"].ToString()) };
            }
        }
        public static Size UrunResimKBoyut
        {
            get
            {
                return new Size { Width = Convert.ToInt32(ConfigurationManager.AppSettings["UrunResimKW"].ToString()), Height = Convert.ToInt32(ConfigurationManager.AppSettings["UrunResimKH"].ToString()) };
            }
        }
        public static Size UrunResimOBoyut
        {
            get
            {
                return new Size { Width = Convert.ToInt32(ConfigurationManager.AppSettings["UrunResimOW"].ToString()), Height = Convert.ToInt32(ConfigurationManager.AppSettings["UrunResimOH"].ToString()) };
            }
        }
    }
}