using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Entities;

namespace WebAutopark.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        int CreateAndReturnId(Order order);
    }
}
