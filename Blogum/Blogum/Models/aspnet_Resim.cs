using System;
using System.Collections.Generic;

namespace Blogum.Models
{
    public partial class aspnet_Resim
    {
        public System.Guid UserId { get; set; }
        public string KucukResimYol { get; set; }
        public string OrtaResimYol { get; set; }
        public string BuyukResimYol { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
    }
}
