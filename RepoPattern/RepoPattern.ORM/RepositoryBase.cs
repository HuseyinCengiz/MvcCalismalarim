using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoPattern.Entity.Models;
namespace RepoPattern.ORM
{
    public class RepositoryBase<TT> : IRepository<TT> where TT : class
    {
        private KuzeyYeliContext _context;

        public KuzeyYeliContext _Context
        {
            get
            {
                _context = _context ?? new KuzeyYeliContext();
                return _context;
            }
        }

        public bool Ekle(TT entity)
        {
            _Context.Set<TT>().Add(entity);
            try
            {
                return _Context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Guncelle()
        {
            return _Context.SaveChanges() > 0;
        }

        public List<TT> Liste()
        {
            return _Context.Set<TT>().ToList();
        }

        public bool Sil(TT entity)
        {
            _Context.Set<TT>().Remove(entity);
            try
            {
                return _Context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
