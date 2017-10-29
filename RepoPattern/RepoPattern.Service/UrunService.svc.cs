using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RepoPattern.DTO;
using RepoPattern.Entity.Models;
using RepoPattern.ORM;

namespace RepoPattern.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UrunService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UrunService.svc or UrunService.svc.cs at the Solution Explorer and start debugging.
    public class UrunService : ServiceBase<UrunRepository, Urunler, UrunDTO>
    {
    }
}
