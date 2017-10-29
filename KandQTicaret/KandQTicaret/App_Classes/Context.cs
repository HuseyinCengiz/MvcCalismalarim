using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KandQTicaret.Models;
namespace KandQTicaret.App_Classes
{
    public class Context
    {
        private static KandQContext db;

        public static KandQContext DB
        {
            get
            {
                if (db == null)
                    db = new KandQContext();
                return db;
            }
        }
    }
}