using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T instance);
        void Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(T instance);
    }
}
