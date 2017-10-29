using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepoPattern.DTO;
using RepoPattern.ORM;
using RepoPattern.Entity.Models;
using RepoPattern.Extension;

namespace RepoPattern.Service
{
    public class ServiceBase<Rep, Entity, DTO> : IService<DTO> where DTO : class where Rep : RepositoryBase<Entity> where Entity : class
    {
        private Rep repository;

        public Rep Repository
        {
            get
            {
                repository = repository ?? Activator.CreateInstance<Rep>();
                return repository;
            }
        }

        public bool Ekle(DTO dto)
        {
           return Repository.Ekle(dto.Donusturucu<Entity>());
        }

        public bool Guncelle()
        {
            return Repository.Guncelle();
        }

        public List<DTO> Liste()
        {
            return Repository.Liste().Select(x => x.Donusturucu<DTO>()).ToList();
        }

        public bool Sil(DTO dto)
        {
            return Repository.Sil(dto.Donusturucu<Entity>());
        }
    }
}