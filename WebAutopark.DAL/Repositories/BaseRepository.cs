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
    public abstract class BaseRepository<T>
    {
        string connectionString = null;
        protected BaseRepository(string conn)
        {
            connectionString = conn;
        }
        protected void CreateBase(T instance, string sqlQueryString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = sqlQueryString;
                db.Execute(sqlQuery, instance);
            }
        }

        protected void DeleteBase(int id, string sqlQueryString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = sqlQueryString;
                db.Execute(sqlQuery, new { id });
            }
        }

        protected T GetByIdBase(int id, string sqlQueryString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<T>(sqlQueryString, new { id }).FirstOrDefault();
            }
        }

        protected IEnumerable<T> GetAllBase(string sqlQueryString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<T>(sqlQueryString).ToList();
            }
        }

        protected void UpdateBase(T instance, string sqlQueryString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = sqlQueryString;
                db.Execute(sqlQuery, instance);
            }
        }
    }
}
