using HaberPortali.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaberPortali.Data.Model;
using System.Linq.Expressions;
using HaberPortali.Data.DataContext;
using System.Data.Entity.Migrations;//AddOrUpdate için gerekli

namespace HaberPortali.Core.Repository
{
    public class ResimRepository : IResimRepository
    {
        private readonly HaberContext _context = new HaberContext();
        public int Count()
        {
            return _context.Resim.Count();
        }

        public void Delete(int id)
        {
            var Resim = GetById(id);
            if (Resim!=null)
            {
                _context.Resim.Remove(Resim);
            }
        }

        public Resim Get(Expression<Func<Resim, bool>> expression)
        {
            return _context.Resim.FirstOrDefault(expression);
        }

        public IEnumerable<Resim> GetAll()
        {
            return _context.Resim.Select(x => x);
        }

        public Resim GetById(int id)
        {
            return _context.Resim.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Resim> GetMany(Expression<Func<Resim, bool>> expression)
        {
            return _context.Resim.Where(expression);
        }

        public void Insert(Resim obj)
        {
            _context.Resim.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Resim obj)
        {
            _context.Resim.AddOrUpdate();
        }
    }
}
