using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogum.Models
{
    public partial class Etiket
    {
        public Etiket()
        {
            this.Makales = new List<Makale>();
        }
        [Key]
        public int Id { get; set; }
        public string EtiketAdi { get; set; }
        public virtual ICollection<Makale> Makales { get; set; }
    }
}
