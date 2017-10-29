using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogum.Models
{
    public partial class Resim
    {
        [Key]
        public int Id { get; set; }
        public string KucukResimYol { get; set; }
        public string OrtaResimYol { get; set; }
        public string BuyukResimYol { get; set; }
        public virtual Makale Makale { get; set; }
    }
}
