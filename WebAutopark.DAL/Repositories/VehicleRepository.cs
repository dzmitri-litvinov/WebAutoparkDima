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
    public class VehicleRepository : RepositoryBase, IRepository<Vehicle>
    {
        private const string sqlQueryCreateString = "INSERT INTO Vehicles (VehicleTypeId, ModelName, RegistrationNumber, WeightKg, " +
            "ManufactureYear, MileageKm, Color, Engine, EngineCapacity, Consumption, FuelTankOrBattery) VALUES(@VehicleTypeId, " +
            "@ModelName, @RegistrationNumber, @WeightKg, @ManufactureYear, @MileageKm, @Color, @Engine, @EngineCapacity, @Consumption, @FuelTankOrBattery)";
        private const string sqlQueryDeleteString = "DELETE FROM Vehicles WHERE Id = @id";
        private const string sqlQueryGetAllString = "SELECT * FROM Vehicles";
        private const string sqlQueryGetByIdString = "SELECT * FROM Vehicles WHERE Id = @id";
        private const string sqlQueryUpdateString = "UPDATE VehicleTypes SET VehicleTypeId = @VehicleTypeId, ModelName = @ModelName, " +
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
            return connection.Query<Vehicle>(sqlQueryGetAllString);
        }

        public Vehicle GetById(int id)
        {
            return connection.QueryFirst<Vehicle>(sqlQueryGetByIdString, new { id }); ;
        }

        public void Update(Vehicle instance)
        {
            connection.Execute(sqlQueryUpdateString, instance);
        }
    }
}