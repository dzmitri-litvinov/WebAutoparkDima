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
    public class VehicleRepository : RepositoryBase, IVehicleRepository
    {
        private const string sqlQueryCreateString = "INSERT INTO Vehicles (VehicleTypeId, ModelName, RegistrationNumber, WeightKg, " +
            "ManufactureYear, MileageKm, Color, Engine, EngineCapacity, Consumption, FuelTankOrBattery) VALUES(@VehicleTypeId, " +
            "@ModelName, @RegistrationNumber, @WeightKg, @ManufactureYear, @MileageKm, @Color, @Engine, @EngineCapacity, @Consumption, @FuelTankOrBattery)";
        private const string sqlQueryDeleteString = "DELETE FROM Vehicles WHERE Id = @id";
        private const string sqlQueryGetAllString = "SELECT V.*, VT.Id VTID, VT.TypeName, VT.TaxCoefficient FROM Vehicles V INNER JOIN VehicleTypes VT ON V.VehicleTypeId = VT.Id";
        private const string sqlQueryGetByIdString = "SELECT V.*, VT.Id VTID, VT.TypeName, VT.TaxCoefficient FROM Vehicles V INNER JOIN VehicleTypes VT ON V.VehicleTypeId = VT.Id WHERE V.Id = @id";
        private const string sqlQueryUpdateString = "UPDATE Vehicles SET VehicleTypeId = @VehicleTypeId, ModelName = @ModelName, " +
            "RegistrationNumber = @RegistrationNumber, WeightKg = @WeightKg, ManufactureYear = @ManufactureYear, MileageKm = @MileageKm, Color = @Color, " +
            "Engine = @Engine, EngineCapacity = @EngineCapacity, Consumption = @Consumption, FuelTankOrBattery = @FuelTankOrBattery WHERE Id = @Id";
        
        public VehicleRepository(string conn) : base(conn)
        { }

        public void Create(Vehicle instance)
        {
            connection.Execute(sqlQueryCreateString, instance);
        }

        public void Delete(int id)
        {
            connection.Execute(sqlQueryDeleteString, new { id });
        }

        public IEnumerable<Vehicle> GetAll()
        {            
            return connection.Query<Vehicle, VehicleType, Vehicle>(sqlQueryGetAllString, 
                (vehicle, vehicleType) => 
                { 
                    vehicle.VehicleType = vehicleType; 
                    return vehicle; 
                }, splitOn: "VTID");
        }

        public Vehicle GetById(int id)
        {
            return connection.Query<Vehicle, VehicleType, Vehicle>(sqlQueryGetByIdString,
                (vehicle, vehicleType) =>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                }, splitOn: "VTID", param: new { id }).FirstOrDefault();
        }

        public void Update(Vehicle instance)
        {
            connection.Execute(sqlQueryUpdateString, instance);
        }

        public IEnumerable<Vehicle> GetAllOrderBy(string orderingCol, OrderingDirection orderingDir)
        {
            return connection.Query<Vehicle, VehicleType, Vehicle>(sqlQueryGetAllString + SqlOrdering(orderingCol, orderingDir),
                (vehicle, vehicleType) =>
                {
                    vehicle.VehicleType = vehicleType;
                    return vehicle;
                }, splitOn: "VTID");
        }

        private string SqlOrdering(string orderingCol, OrderingDirection orderingDir)
        {
            string sqlOrdering = "";

            switch (orderingCol)
            {
                case "id":
                    sqlOrdering += " ORDER BY V.Id";
                    break;
                case "modelName":
                    sqlOrdering += " ORDER BY V.ModelName";
                    break;
                case "vehicleType":
                    sqlOrdering += " ORDER BY VT.TypeName";
                    break;
                case "mileageKm":
                    sqlOrdering += " ORDER BY V.MileageKm";
                    break;
                default:
                    break;
            }

            switch (orderingDir)
            {
                case OrderingDirection.ASC:
                    sqlOrdering += " ASC";
                    break;
                case OrderingDirection.DESC:
                    sqlOrdering += " DESC";
                    break;
                default:
                    break;
            }

            return sqlOrdering;
        }
    }
}