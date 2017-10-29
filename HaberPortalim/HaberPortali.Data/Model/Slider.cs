using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberPortali.Data.Model
{
    [Table("Slider")]
    public class Slider:BaseEntity
    {
        [Display(Name ="Başlık")]
        [MinLength(3,ErrorMessage ="En az {0} Karakter Olabilir !"),MaxLength(255,ErrorMessage ="{1} Karakterden fazla olamaz")]
        public string Baslik { get; set; }

        [Display(Name = "URL")]
        [MinLength(3, ErrorMessage = "En az {0} Karakter Olabilir !"), MaxLength(255, ErrorMessage = "{1} Karakterden fazla olamaz")]
        public string URL { get; set; }

        [Display(Name = "Açıklama")]
        [MinLength(3, ErrorMessage = "En az {0} Karakter Olabilir !"), MaxLength(255, ErrorMessage = "{1} Karakterden fazla olamaz")]
        public string  Aciklama { get; set; }
        [Display(Name = "Resim")]
        [MinLength(3, ErrorMessage = "En az {0} Karakter Olabilir !"), MaxLength(255, ErrorMessage = "{1} Karakterden fazla olamaz")]
        [Required(ErrorMessage ="Zorunlu Alan")]
        public string ResimURL { get; set; }
    }
}
