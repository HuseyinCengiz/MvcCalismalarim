using HaberPortali.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberPortali.Core.Infrastructure
{
    public interface IEtiketRepository : IRepository<Etiket>
    {
        IQueryable<Etiket> Etiketler(string[] etiketler);
        void EtiketEkle(int HaberID, string etiket);
        void HaberEtiketEkle(int HaberID, string[] etiketler);
    }
}
