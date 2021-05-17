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
    public class VehicleTypesRepository : IRepository<VehicleType>
    {
        string connectionString = null;
        public VehicleTypesRepository(string conn)
        {
            connectionString = conn;
        }

        public void Create(VehicleType instance)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO VehicleTypes (TypeName, TaxCoefficient) VALUES(@TypeName, @TaxCoefficient)";
                db.Execute(sqlQuery, instance);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM VehicleTypes WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public VehicleType Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<VehicleType>("SELECT * FROM VehicleTypes WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<VehicleType> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<VehicleType>("SELECT * FROM VehicleTypes").ToList();
            }
        }

        public void Update(VehicleType instance)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE VehicleTypes SET TypeName = @TypeName, TaxCoefficient = @TaxCoefficient WHERE Id = @Id";
                db.Execute(sqlQuery, instance);
            }
        }
    }
}
