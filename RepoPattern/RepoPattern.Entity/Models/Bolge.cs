using System;
using System.Collections.Generic;

namespace RepoPattern.Entity.Models
{
    public partial class Bolge
    {
        public Bolge()
        {
            this.Bolgelers = new List<Bolgeler>();
        }

        public int BolgeID { get; set; }
        public string BolgeTanimi { get; set; }
        public virtual ICollection<Bolgeler> Bolgelers { get; set; }
    }
}
