using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberPortali.Data.Model
{
    [Table("Haber")]
    public class Haber : BaseEntity
    {

        [Display(Name = "Haber Başlık")]
        [MaxLength(255, ErrorMessage = "Çok fazla girdiniz !")]
        [Required]
        public string Baslik { get; set; }
        [Display(Name = "Kısa Açıklama")]
        public string KisaAciklama { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        public int Okunma { get; set; }
        public int KullaniciID { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        [Display(Name = "Resim")]
        [MaxLength(255, ErrorMessage = "Çok fazla girdiniz !")]
        public string Resim { get; set; }
        public virtual ICollection<Resim> Resims { get; set; }
        public int KategoriID { get; set; }
        public virtual Kategori Kategori { get; set; }

        public virtual ICollection<Etiket> Etiket { get; set; }
    }
}