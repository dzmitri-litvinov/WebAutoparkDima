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
    public class OrderElementsRepository : RepositoryBase, IOrderElementsRepository
    {
        private const string sqlQueryCreateString = "INSERT INTO OrderElements (OrderId, SparePartId, SparePartQuantity) VALUES(@OrderId, @SparePartId, @SparePartQuantity)";
        private const string sqlQueryDeleteString = "DELETE FROM OrderElements WHERE Id = @id";
        private const string sqlQueryGetAllString = "SELECT OE.*, SP.Id SPID, SP.PartName FROM OrderElements OE JOIN SpareParts SP ON OE.SparePartId = SP.Id";
        private const string sqlQueryGetAllByOrderIdString = "SELECT OE.*, SP.Id SPID, SP.PartName FROM OrderElements OE JOIN SpareParts SP ON OE.SparePartId = SP.Id WHERE OE.OrderId = @Id";
        private const string sqlQueryGetByIdString = "SELECT * FROM OrderElements WHERE Id = @id";
        private const string sqlQueryUpdateString = "UPDATE OrderElements SET OrderId = @OrderId, SparePartId = @SparePartId, SparePartQuantity = @SparePartQuantity WHERE Id = @Id";

        public OrderElementsRepository(string conn) : base(conn)
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
            return connection.Query<OrderElement, SparePart, OrderElement>(sqlQueryGetAllString,
                (orderElement, sparePart) =>
                {
                    orderElement.SparePart = sparePart;
                    return orderElement;
                }, splitOn: "SPID");
        }

        public OrderElement GetById(int id)
        {
            return connection.QueryFirst<OrderElement>(sqlQueryGetByIdString, new { id });
        }

        public void Update(OrderElement instance)
        {
            connection.Execute(sqlQueryUpdateString, instance);
        }

        public IEnumerable<OrderElement> GetAllByOrderId(int id)
        {
            return connection.Query<OrderElement, SparePart, OrderElement>(sqlQueryGetAllByOrderIdString,
                (orderElement, sparePart) =>
                {
                    orderElement.SparePart = sparePart;
                    return orderElement;
                }, splitOn: "SPID", param: new { id });
        }
    }
}
