using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface;

namespace DataAccessEF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApiDemoContext db;
        public GenericRepository(ApiDemoContext db)
        {
            this.db = db;
        }

        public T Create(T entity)
        {
            db.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public T Update(T entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return entity;
        }
    }
}
