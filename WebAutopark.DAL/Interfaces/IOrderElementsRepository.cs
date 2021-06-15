using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Entities;

namespace WebAutopark.DAL.Interfaces
{
    public interface IOrderElementsRepository : IRepository<OrderElement>
    {
        IEnumerable<OrderElement> GetAllByOrderId(int id);
    }
}
