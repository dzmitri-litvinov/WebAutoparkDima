using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace WebAutopark.DAL.Repositories
{
    public abstract class BaseRepository : IDisposable, IAsyncDisposable
    {
        protected readonly DbConnection connection;
        protected BaseRepository(IConnectionProvider connectionProvider)
        {
            connection = connectionProvider.GetConnection();
        }
        
        public void Dispose()
        {
            connection?.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return connection.DisposeAsync();
        }
    }
}
