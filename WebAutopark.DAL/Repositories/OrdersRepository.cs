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
    public class OrdersRepository : RepositoryBase, IRepository<Order>
    {
        private const string sqlQueryCreateString = "INSERT INTO Orders (VehicleId) VALUES(@VehicleId)";
        private const string sqlQueryDeleteString = "DELETE FROM Orders WHERE Id = @id";
        private const string sqlQueryGetAllString = "SELECT * FROM Orders";
        private const string sqlQueryGetByIdString = "SELECT * FROM Orders WHERE Id = @id";
        private const string sqlQueryUpdateString = "UPDATE Orders SET VehicleId = @VehicleId WHERE Id = @Id";

        public OrdersRepository(string conn) : base(conn)
        { }

        public void Create(Order instance)
        {
            connection.Execute(sqlQueryCreateString, instance);
        }

        public void Delete(int id)
        {
            connection.Execute(sqlQueryDeleteString, new { id });
        }

        public IEnumerable<Order> GetAll()
        {
            return connection.Query<Order>(sqlQueryGetAllString);
        }

        public Order GetById(int id)
        {
            return connection.QueryFirst<Order>(sqlQueryGetByIdString, new { id });
        }

        public void Update(Order instance)
        {
            connection.Execute(sqlQueryUpdateString, instance);
        }
    }
}
