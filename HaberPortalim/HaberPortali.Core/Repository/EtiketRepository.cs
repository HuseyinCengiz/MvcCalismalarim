using HaberPortali.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaberPortali.Data.Model;
using System.Linq.Expressions;
using HaberPortali.Data.DataContext;
using System.Data.Entity.Migrations;

namespace HaberPortali.Core.Repository
{
    public class EtiketRepository : IEtiketRepository
    {
        private readonly HaberContext _context = new HaberContext();
        public int Count()
        {
            return _context.Etiket.Count();
        }

        public void Delete(int id)
        {
            Etiket etiket = GetById(id);
            if (etiket != null)
            {
                _context.Etiket.Remove(etiket);
            }
        }

        public IQueryable<Etiket> Etiketler(string[] etiketler)
        {
            return _context.Etiket.Where(x => etiketler.Contains(x.EtiketAdi));
        }

        public Etiket Get(Expression<Func<Etiket, bool>> expression)
        {
            return _context.Etiket.FirstOrDefault(expression);
        }

        public IEnumerable<Etiket> GetAll()
        {
            return _context.Etiket.Select(x => x);
        }

        public Etiket GetById(int id)
        {
            return _context.Etiket.Where(i => i.ID == id).FirstOrDefault();
        }

        public IQueryable<Etiket> GetMany(Expression<Func<Etiket, bool>> expression)
        {
            return _context.Etiket.Where(expression);
        }

        public void EtiketEkle(int HaberID, string etiket)
        {
            //tolist listeye alıyor
            /*  var Haber = _context.Haber.FirstOrDefault(x => x.ID == HaberID);
              var gelenEtiket = this.Etiketler(etiket);
              Haber.Etiket.Clear();
              gelenEtiket.ToList().ForEach(etiket => Haber.Etiket.Add(etiket));
              _context.SaveChanges();*/
            if (etiket != null && etiket != "")
            {
                string[] etiketler = etiket.Split(',');
                foreach (var tag in etiketler)
                {
                    Etiket etiketim = this.Get(x => x.EtiketAdi.ToLower() == tag.ToLower());
                    if (etiketim == null)
                    {
                        etiketim = new Etiket();
                        etiketim.EtiketAdi = tag;
                        this.Insert(etiketim);
                        this.Save();
                    }
                }
                this.HaberEtiketEkle(HaberID, etiketler);
            }
        }
        public void Insert(Etiket obj)
        {
            _context.Etiket.Add(obj);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(Etiket obj)
        {
            _context.Etiket.AddOrUpdate();
        }
        public void HaberEtiketEkle(int HaberID, string[] etiketler)
        {
            var Haber = _context.Haber.FirstOrDefault(x => x.ID == HaberID);
            var gelenetiket = this.Etiketler(etiketler);
            Haber.Etiket.Clear();
            gelenetiket.ToList().ForEach(etkt => Haber.Etiket.Add(etkt));
            _context.SaveChanges();
        }
    }
}
