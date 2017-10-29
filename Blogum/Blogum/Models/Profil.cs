using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Blogum.Models
{
    public partial class Profil
    {
        public System.Guid UserID { get; set; }
        [AllowHtml]
        public string Biyografim { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Google { get; set; }
        public string LinkedIn { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
    }
}
