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
    public class SparePartsRepository : IRepository<SparePart>
    {
        string connectionString = null;
        public SparePartsRepository(string conn)
        {
            connectionString = conn;
        }

        public void Create(SparePart instance)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO SpareParts (PartName) VALUES(@PartName)";
                db.Execute(sqlQuery, instance);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM SpareParts WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public SparePart Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<SparePart>("SELECT * FROM SpareParts WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<SparePart> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<SparePart>("SELECT * FROM SpareParts").ToList();
            }
        }

        public void Update(SparePart instance)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE SpareParts SET PartName = @PartName WHERE Id = @Id";
                db.Execute(sqlQuery, instance);
            }
        }
    }
}
