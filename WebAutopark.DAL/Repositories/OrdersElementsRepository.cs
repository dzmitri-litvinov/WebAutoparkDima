using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;
using Dapper;

namespace WebAutopark.DAL.Repositories
{
    public class OrdersElementsRepository : RepositoryBase, IRepository<OrderElement>
    {
        private const string sqlQueryCreateString = "INSERT INTO OrdersElements (OrderId, SparePartId, SparePartQuantity) VALUES(@OrderId, @SparePartId, @SparePartQuantity)";
        private const string sqlQueryDeleteString = "DELETE FROM OrdersElements WHERE Id = @id";
        private const string sqlQueryGetAllString = "SELECT * FROM OrdersElements";
        private const string sqlQueryGetByIdString = "SELECT * FROM OrdersElements WHERE Id = @id";
        private const string sqlQueryUpdateString = "UPDATE OrdersElements SET OrderId = @OrderId, SparePartId = @SparePartId, SparePartQuantity = @SparePartQuantity WHERE Id = @Id";

        public OrdersElementsRepository(string conn) : base(conn)
        { }

        public void Create(OrderElement instance)
        {
            connection.Execute(sqlQueryCreateString, instance);
        }

        public void Delete(int id)
        {
            connection.Execute(sqlQueryDeleteString, new { id });
        }

        public IEnumerable<OrderElement> GetAll()
        {
            return connection.Query<OrderElement>(sqlQueryGetAllString);
        }

        public OrderElement GetById(int id)
        {
            return connection.QueryFirst<OrderElement>(sqlQueryGetByIdString, new { id });
        }

        public void Update(OrderElement instance)
        {
            connection.Execute(sqlQueryUpdateString, instance);
        }
    }
}
