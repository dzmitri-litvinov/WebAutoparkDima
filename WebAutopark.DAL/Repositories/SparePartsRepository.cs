using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;

namespace WebAutopark.DAL.Repositories
{
    public class SparePartsRepository : BaseRepository<SparePart>, IRepository<SparePart>
    {
        private const string sqlQueryCreateString = "INSERT INTO SpareParts (PartName) VALUES(@PartName)";
        private const string sqlQueryDeleteString = "DELETE FROM SpareParts WHERE Id = @id";
        private const string sqlQueryGetAllString = "SELECT * FROM SpareParts";
        private const string sqlQueryGetByIdString = "SELECT * FROM SpareParts WHERE Id = @id";
        private const string sqlQueryUpdateString = "UPDATE SpareParts SET PartName = @PartName WHERE Id = @Id";
        public SparePartsRepository(string conn) : base(conn)
        {   }
        public async Task Create(SparePart instance)
        {
            CreateBase(instance, sqlQueryCreateString);
        }

        public async Task Delete(int id)
        {
            DeleteBase(id, sqlQueryDeleteString);
        }

        public async Task<IEnumerable<SparePart>> GetAll()
        {
            return GetAllBase(sqlQueryGetAllString);
        }

        public async Task<SparePart> GetById(int id)
        {
            return GetByIdBase(id, sqlQueryGetByIdString);
        }

        public async Task Update(SparePart instance)
        {
            UpdateBase(instance, sqlQueryUpdateString);
        }
    }
}
