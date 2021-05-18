using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;

namespace WebAutopark.DAL.Repositories
{
    public class VehicleTypesRepository : BaseRepository<VehicleType>, IRepository<VehicleType>
    {
        private const string sqlQueryCreateString = "INSERT INTO VehicleTypes (TypeName, TaxCoefficient) VALUES(@TypeName, @TaxCoefficient)";
        private const string sqlQueryDeleteString = "DELETE FROM VehicleTypes WHERE Id = @id";
        private const string sqlQueryGetAllString = "SELECT * FROM VehicleTypes";
        private const string sqlQueryGetByIdString = "SELECT * FROM VehicleTypes WHERE Id = @id";
        private const string sqlQueryUpdateString = "UPDATE VehicleTypes SET TypeName = @TypeName, TaxCoefficient = @TaxCoefficient WHERE Id = @Id";
        public VehicleTypesRepository(string conn) : base(conn)
        { }
        public async Task Create(VehicleType instance)
        {
            CreateBase(instance, sqlQueryCreateString);
        }

        public async Task Delete(int id)
        {
            DeleteBase(id, sqlQueryDeleteString);
        }

        public async Task<IEnumerable<VehicleType>> GetAll()
        {
            return GetAllBase(sqlQueryGetAllString);
        }

        public async Task<VehicleType> GetById(int id)
        {
            return GetByIdBase(id, sqlQueryGetByIdString);
        }

        public async Task Update(VehicleType instance)
        {
            UpdateBase(instance, sqlQueryUpdateString);
        }
    }
}