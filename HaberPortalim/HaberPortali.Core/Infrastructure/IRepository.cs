using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HaberPortali.Core.Infrastructure
{
    /*IEnumerable ve IQueryable arasındaki temel fark, IEnumerable tüm verileri alıp memory de tutarak, sorgulama işlemlerini memory üzerinden yaparken IQueryable ise şartlara bağlı query oluşturarak doğrudan veritabanı üzerinden sorgulama işlemi yapar. Eğer milyonlarca kayıt üzerinde sorgulama işlemi yapıyorsak elbette IQueryable IEnumerable göre daha hızlı sorgulama işlemi yapar.*/
    public interface IRepository<T> where T : class
    {
        //Tüm Datalarımızı çekecek
        IEnumerable<T> GetAll();
        //tek bir nesne dönecek
        T GetById(int id);

        //Burda expression göndererek gönderdiğimiz sorguya göre veri alıyoruz
        T Get(Expression<Func<T, bool>> expression);

        IQueryable<T> GetMany(Expression<Func<T, bool>> expression);

        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
        int Count();
        void Save();
    }
}
