using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberPortali.Data.Model
{
    [Table("Kullanici")]//veritabanındaki tabloya s takısı eklemez
    public class Kullanici : BaseEntity
    {

        //Maxlengt 150 karakterden fazla giremiyoruz
        [MaxLength(150, ErrorMessage = "Lütfen 150 karakterden fazla değer girmeyiniz ! ")]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Display(Name = "E-mail")]
        [RegularExpression(@"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$", ErrorMessage = "Geçerli Bir Mail Adresi Giriniz!")]
        public string Email { get; set; }
        [Display(Name = "Şifre")]//Görüntülerken isim böyle gözüküyor
        [DataType(DataType.Password)]
        [Required]//Zorunlu yapıyoruz
        [MaxLength(16, ErrorMessage = "Lütfen 16 karakterden fazla değer girmeyiniz ! ")]
        public string Sifre { get; set; }
        public virtual Rol Rol { get; set; }

    }
}
