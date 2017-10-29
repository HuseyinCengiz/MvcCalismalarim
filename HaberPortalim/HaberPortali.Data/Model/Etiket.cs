using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberPortali.Data.Model
{
    [Table("Etiket")]
    public class Etiket : BaseEntity
    {
        public string EtiketAdi { get; set; }

        public virtual ICollection<Haber> Haber { get; set; }
    }
}
