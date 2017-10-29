using System;
using System.Collections.Generic;

namespace Blogum.Models
{
    public partial class vw_aspnet_Applications
    {
        public string ApplicationName { get; set; }
        public string LoweredApplicationName { get; set; }
        public System.Guid ApplicationId { get; set; }
        public string Description { get; set; }
    }
}
