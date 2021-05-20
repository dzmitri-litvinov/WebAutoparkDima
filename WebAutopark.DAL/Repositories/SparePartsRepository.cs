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
    public class SparePartsRepository : RepositoryBase, IRepository<SparePart>
    {
        private const string sqlQueryCreateString = "INSERT INTO SpareParts (PartName) VALUES(@PartName)";
        private const string sqlQueryDeleteString = "DELETE FROM SpareParts WHERE Id = @id";
        private const string sqlQueryGetAllString = "SELECT * FROM SpareParts";
        private const string sqlQueryGetByIdString = "SELECT * FROM SpareParts WHERE Id = @id";
        private const string sqlQueryUpdateString = "UPDATE SpareParts SET PartName = @PartName WHERE Id = @Id";
        
        public SparePartsRepository(string conn) : base(conn)
        {   }

        public void Create(SparePart instance)
        {
            connection.Execute(sqlQueryCreateString, instance);
        }

        public void Delete(int id)
        {
            connection.Execute(sqlQueryDeleteString, new { id });
        }

        public IEnumerable<SparePart> GetAll()
        {
            return connection.Query<SparePart>(sqlQueryGetAllString);
        }

        public SparePart GetById(int id)
        {
            return connection.QueryFirst<SparePart>(sqlQueryGetByIdString, new { id });
        }

        public void Update(SparePart instance)
        {
            connection.Execute(sqlQueryUpdateString, instance);
        }
    }
}
