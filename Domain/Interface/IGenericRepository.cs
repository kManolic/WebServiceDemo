using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    // Interface for basic CRUD operations
    public interface IGenericRepository<T> where T : class 
    {
        T GetById(int id);
        List<T> GetAll();
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);

    }
}
