using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace RepoPattern.Service
{
    [ServiceContract]
    public interface IService<DTO> where DTO : class
    {
        [OperationContract]
        List<DTO> Liste();
        [OperationContract]
        bool Ekle(DTO dto);
        [OperationContract]
        bool Guncelle();
        [OperationContract]
        bool Sil(DTO dto);

    }
}
