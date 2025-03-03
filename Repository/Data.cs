using System.Data;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Test_API.Repository
{
    public class Data : IData
    {
        private readonly string _connectionString;

        public Data()
        {
            string server = Environment.GetEnvironmentVariable("Host");
            string database = Environment.GetEnvironmentVariable("Database_name");
            string user = Environment.GetEnvironmentVariable("Database_user");
            string password = Environment.GetEnvironmentVariable("Database_password");

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database) ||
                string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("Database connection details are missing in environemnt variables");
            }
            _connectionString = $"Server={server};Database={database};User Id={user};Password={password};";
        }
        public async Task<IEnumerable<T>> GetDataAsync<T>(string tableName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM {tableName}";
                return await db.QueryAsync<T>(query);
            }
        }
        public async Task<int> ExecuteCommandAsync(string query, object parameters = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.ExecuteAsync(query, parameters);
            }
        }
    }
}
