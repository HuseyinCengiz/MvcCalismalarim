using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberPortali.Data.Model
{
    public class HaberEtiketModel
    {
        public Haber Haber { get; set; }
        public string[] GelenEtiketler { get; set; }
        public IEnumerable<Etiket> Etiketler { get; set; }
        public string EtiketAdi { get; set; }
    }
}
