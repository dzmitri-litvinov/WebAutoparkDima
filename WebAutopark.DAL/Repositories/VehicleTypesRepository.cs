using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;
using Dapper;

namespace WebAutopark.DAL.Repositories
{
    public class VehicleTypesRepository : RepositoryBase, IRepository<VehicleType>
    {
        private const string sqlQueryCreateString = "INSERT INTO VehicleTypes (TypeName, TaxCoefficient) VALUES(@TypeName, @TaxCoefficient)";
        private const string sqlQueryDeleteString = "DELETE FROM VehicleTypes WHERE Id = @id";
        private const string sqlQueryGetAllString = "SELECT * FROM VehicleTypes";
        private const string sqlQueryGetByIdString = "SELECT * FROM VehicleTypes WHERE Id = @id";
        private const string sqlQueryUpdateString = "UPDATE VehicleTypes SET TypeName = @TypeName, TaxCoefficient = @TaxCoefficient WHERE Id = @Id";
        
        public VehicleTypesRepository(string conn) : base(conn)
        { }
        
        public void Create(VehicleType instance)
        {
            connection.Execute(sqlQueryCreateString, instance);
        }

        public void Delete(int id)
        {
            connection.Execute(sqlQueryDeleteString, new { id });
        }

        public IEnumerable<VehicleType> GetAll()
        {
            return connection.Query<VehicleType>(sqlQueryGetAllString);
        }

        public VehicleType GetById(int id)
        {
            return connection.QueryFirst<VehicleType>(sqlQueryGetByIdString, new { id });
        }

        public void Update(VehicleType instance)
        {
            connection.Execute(sqlQueryUpdateString, instance);
        }
    }
}