using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class SatisOzellik
    {
        public int Id { get; set; }
        public Nullable<int> OzellikTipID { get; set; }
        public Nullable<int> OzellikDegerID { get; set; }
        public Nullable<int> SatisDetayID { get; set; }
        public virtual OzellikDeger OzellikDeger { get; set; }
        public virtual OzellikTip OzellikTip { get; set; }
        public virtual SatisDetay SatisDetay { get; set; }
    }
}
