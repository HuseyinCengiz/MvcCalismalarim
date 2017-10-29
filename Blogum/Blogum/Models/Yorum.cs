using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogum.Models
{
    public partial class Yorum
    {
        [Key]
        public int Id { get; set; }
        public string Icerik { get; set; }
        public string Email { get; set; }
        public string AdSoyad { get; set; }
        public int MakaleID { get; set; }
        public Nullable<System.Guid> YazanID { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual Makale Makale { get; set; }
    }
}
