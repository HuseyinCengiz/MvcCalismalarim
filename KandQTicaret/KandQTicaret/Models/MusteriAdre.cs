using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class MusteriAdre
    {
        public System.Guid UserID { get; set; }
        public string Adres { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
    }
}
