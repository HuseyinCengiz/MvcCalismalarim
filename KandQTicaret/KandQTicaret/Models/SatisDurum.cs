using System;
using System.Collections.Generic;

namespace KandQTicaret.Models
{
    public partial class SatisDurum
    {
        public SatisDurum()
        {
            this.Satis = new List<Sati>();
        }

        public int Id { get; set; }
        public string Aciklama { get; set; }
        public virtual ICollection<Sati> Satis { get; set; }
    }
}
