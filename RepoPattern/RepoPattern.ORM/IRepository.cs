using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattern.ORM
{
    public interface IRepository<T> where T : class
    {
        List<T> Liste();
        bool Ekle(T entity);
        bool Guncelle();
        bool Sil(T entity);
    }
}
