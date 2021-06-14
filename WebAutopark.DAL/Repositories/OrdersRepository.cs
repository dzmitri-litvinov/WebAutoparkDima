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
    public class OrdersRepository : RepositoryBase, IOrderRepository
    {
        private const string sqlQueryCreateString = "INSERT INTO Orders (VehicleId) VALUES(@VehicleId)";
        private const string sqlQueryCreateAndReturnIdString = "INSERT INTO Orders (VehicleId) VALUES(@VehicleId); SELECT CAST(SCOPE_IDENTITY() as int)";
        private const string sqlQueryDeleteString = "DELETE FROM Orders WHERE Id = @id";
        private const string sqlGettAllString = "SELECT O.*, " +
                                                    "V.Id VID, V.VehicleTypeId, V.ModelName, V.RegistrationNumber, V.WeightKg, " +
                                                    "V.ManufactureYear, V.MileageKm, V.Color, V.Engine, V.EngineCapacity, V.Consumption, V.FuelTankOrBattery, " +
                                                    "OE.Id OEID, OE.OrderId, OE.SparePartId, OE.SparePartQuantity, " +
                                                    "SP.ID SPID, SP.PartName " +
                                                    "FROM Orders O " +
                                                        "FULL JOIN OrdersElements OE ON O.Id = OE.OrderId " +
                                                        "JOIN Vehicles V ON O.VehicleId = V.Id " +
                                                        "LEFT JOIN SpareParts SP ON OE.SparePartId = SP.Id";
        private const string sqlGetAllOrderWithoutElements = "SELECT O.*, V.Id VID, V.VehicleTypeId, V.ModelName, V.RegistrationNumber, V.WeightKg, V.ManufactureYear, " +
                                                                "V.MileageKm, V.Color, V.Engine, V.EngineCapacity, V.Consumption, V.FuelTankOrBattery " +
                                                                "FROM Orders O " +
                                                                "JOIN Vehicles V ON O.VehicleId = V.Id";
        private const string sqlGettByIdString = "SELECT O.*, " +
                                                    "V.Id VID, V.VehicleTypeId, V.ModelName, V.RegistrationNumber, V.WeightKg, " +
                                                    "V.ManufactureYear, V.MileageKm, V.Color, V.Engine, V.EngineCapacity, V.Consumption, V.FuelTankOrBattery, " +
                                                    "OE.Id OEID, OE.OrderId, OE.SparePartId, OE.SparePartQuantity, " +
                                                    "SP.ID SPID, SP.PartName " +
                                                    "FROM Orders O " +
                                                        "FULL JOIN OrdersElements OE ON O.Id = OE.OrderId " +
                                                        "JOIN Vehicles V ON O.VehicleId = V.Id " +
                                                        "LEFT JOIN SpareParts SP ON OE.SparePartId = SP.Id WHERE O.Id = @id";

        private const string sqlGetByIdOrderWithoutElements = "SELECT O.*, V.Id VID, V.VehicleTypeId, V.ModelName, V.RegistrationNumber, V.WeightKg, V.ManufactureYear, " +
                                                                "V.MileageKm, V.Color, V.Engine, V.EngineCapacity, V.Consumption, V.FuelTankOrBattery " +
                                                                "FROM Orders O " +
                                                                "JOIN Vehicles V ON O.VehicleId = V.Id WHERE O.Id = @id";
        private const string sqlQueryUpdateString = "UPDATE Orders SET VehicleId = @VehicleId WHERE Id = @Id";

        public OrdersRepository(string conn) : base(conn)
        { }

        public void Create(Order instance)
        {
            connection.Execute(sqlQueryCreateString, instance);
        }

        public int CreateAndReturnId(Order order)
        {
            return connection.QuerySingle<int>(sqlQueryCreateAndReturnIdString, order);
        }

        public void Delete(int id)
        {
            connection.Execute(sqlQueryDeleteString, new { id });
        }

        public IEnumerable<Order> GetAll()
        {
            return connection.Query<Order, Vehicle, Order>(sqlGetAllOrderWithoutElements,
                    (order, vehicle) =>
                    {
                        order.Vehicle = vehicle;
                        return order;
                    }, splitOn: "VID");
        }

        public Order GetById(int id)
        {
            var coll = connection.Query<Order, Vehicle, OrderElement, SparePart, Order>(sqlGettByIdString,
                (order, vehicle, orderElement, sparePart) =>
                {
                    order.Vehicle = vehicle;
                    orderElement.SparePart = sparePart;
                    order.OrderElements = new List<OrderElement>() { orderElement };
                    return order;
                }, splitOn: "VID,OEID,SPID", param: new { id });

            var result = coll.GroupBy(e => e.Id).Select(g =>
                {
                    var groupedOrder = g.First();
                    groupedOrder.OrderElements = g.Select(e => e.OrderElements.First()).ToList();
                    return groupedOrder;
                }).FirstOrDefault();

            if (result.OrderElements.FirstOrDefault().OrderId == 0)
            {
                result.OrderElements = null;
            }

            return result;
        }

        public void Update(Order instance)
        {
            connection.Execute(sqlQueryUpdateString, instance);
        }
    }
}
