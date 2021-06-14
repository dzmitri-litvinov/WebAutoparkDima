using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Entities;

namespace WebAutopark.DAL.Interfaces
{
    public interface IOrdersElementsRepository : IRepository<OrderElement>
    {
        IEnumerable<OrderElement> GettAllByOrderId(int id);
    }
}
