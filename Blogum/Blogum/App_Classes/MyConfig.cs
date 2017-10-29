using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Blogum.App_Classes
{
    public static class MyConfig
    {
        public static Size ProfilResimBoyut
        {
            get
            {
                return new Size(Convert.ToInt32(ConfigurationManager.AppSettings["ProfilResimW"]), Convert.ToInt32(ConfigurationManager.AppSettings["ProfilResimH"]));
            }
        }
        public static Size MakaleKucukResimBoyut
        {
            get
            {
                return new Size(Convert.ToInt32(ConfigurationManager.AppSettings["MakaleKucukResimW"]), Convert.ToInt32(ConfigurationManager.AppSettings["MakaleKucukResimH"]));
            }
        }
        public static Size MakaleOrtaResimBoyut
        {
            get
            {
                return new Size(Convert.ToInt32(ConfigurationManager.AppSettings["MakaleOrtaResimW"]), Convert.ToInt32(ConfigurationManager.AppSettings["MakaleOrtaResimH"]));
            }
        }
        public static Size MakaleBuyukResimBoyut
        {
            get
            {
                return new Size(Convert.ToInt32(ConfigurationManager.AppSettings["MakaleBuyukResimW"]), Convert.ToInt32(ConfigurationManager.AppSettings["MakaleBuyukResimH"]));
            }
        }
    }
}