using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blogum.Models
{
    public partial class Makale
    {
        public Makale()
        {
            this.Yorums = new List<Yorum>();
            this.Etikets = new List<Etiket>();
        }
        [Key]
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string KisaAciklama { get; set; }
        [AllowHtml]
        public string Aciklama { get; set; }
        public int BegenmeSayisi { get; set; }
        public int OkunmaSayisi { get; set; }
        public System.DateTime EklenmeTarihi { get; set; }
        public System.Guid YazarID { get; set; }
        public int KategoriID { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual Resim Resim { get; set; }
        public virtual ICollection<Yorum> Yorums { get; set; }
        public virtual ICollection<Etiket> Etikets { get; set; }
    }
}
