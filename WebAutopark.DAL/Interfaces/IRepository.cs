using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Create(T instance);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Update(T instance);
    }
}
