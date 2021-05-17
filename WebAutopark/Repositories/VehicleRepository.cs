using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.Interfaces;

namespace WebAutopark.Repositories
{
    public class VehicleRepository : IRepository<Vehicle>
    {
        string connectionString = null;
        public VehicleRepository(string conn)
        {
            connectionString = conn;
        }

        public void Create(Vehicle instance)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Vehicles (VehicleTypeId, ModelName, RegistrationNumber, WeightKg, " +
                    "ManufactureYear, MileageKm, Color, Engine, EngineCapacity, Consumption, FuelTankOrBattery) " +
                    "VALUES(@VehicleTypeId, @ModelName, @RegistrationNumber, " +
                    "@WeightKg, @ManufactureYear, @MileageKm, @Color, @Engine, @EngineCapacity, @Consumption, @FuelTankOrBattery)";
                db.Execute(sqlQuery, instance);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Vehicles WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Vehicle Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Vehicle>("SELECT * FROM Vehicles WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<Vehicle> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Vehicle>("SELECT * FROM Vehicles").ToList();
            }
        }

        public void Update(Vehicle instance)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE VehicleTypes SET VehicleTypeId = @VehicleTypeId, ModelName = @ModelName, " +
                    "RegistrationNumber = @RegistrationNumber, WeightKg = @WeightKg, " +
                    "ManufactureYear = @ManufactureYear, MileageKm = @MileageKm, Color = @Color, " +
                    "Engine = @Engine, EngineCapacity = @EngineCapacity, Consumption = @Consumption, " +
                    "FuelTankOrBattery = @FuelTankOrBattery WHERE Id = @Id";
                db.Execute(sqlQuery, instance);
            }
        }
    }
}
