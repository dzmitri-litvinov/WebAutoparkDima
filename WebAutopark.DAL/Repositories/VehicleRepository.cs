using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;

namespace WebAutopark.DAL.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IRepository<Vehicle>
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
        public async Task Create(Vehicle instance)
        {
            CreateBase(instance, sqlQueryCreateString);
        }

        public async Task Delete(int id)
        {
            DeleteBase(id, sqlQueryDeleteString);
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return GetAllBase(sqlQueryGetAllString);
        }

        public async Task<Vehicle> GetById(int id)
        {
            return GetByIdBase(id, sqlQueryGetByIdString);
        }

        public async Task Update(Vehicle instance)
        {
            UpdateBase(instance, sqlQueryUpdateString);
        }
    }
}