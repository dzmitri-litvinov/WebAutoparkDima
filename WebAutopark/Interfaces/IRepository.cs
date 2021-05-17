using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutopark.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T instance);
        void Delete(int id);
        T Get(int id);
        List<T> GetAll();
        void Update(T instance);
    }
}
