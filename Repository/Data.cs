using System.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Test_API.Objects; // Make sure you have this NuGet package

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
                throw new InvalidOperationException("Database connection details are missing in environment variables");
            }

            _connectionString = $"Host={server};Database={database};Username={user};Password={password}";
        }

        public async Task<IEnumerable<T>> GetDataAsync<T>(string tableName)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM {tableName}";
                return await db.QueryAsync<T>(query);
            }
        }

        public async Task<int> ExecuteCommandAsync(string query, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteScalarAsync<int>(query, parameters);
                return result;
            }
        }
        public async Task<int> AddLocationAsync(Location location)
        {
            string tableName = Environment.GetEnvironmentVariable("Location_Table");
            string query = $"INSERT INTO {tableName} (Setting) VALUES (@Setting) RETURNING ID;";
            var parameters = new { Setting = location.Setting };
            return await ExecuteCommandAsync(query, parameters);
        }
        public async Task<int> AddTemperatureAsync(Temperature temperature)
        {
            string tableName = Environment.GetEnvironmentVariable("Temperature_Table");
            string query = $"INSERT INTO {tableName} (Value, Setting) VALUES (@Value, @Setting) RETURNING ID;";
            var parameters = new { Value = temperature.Value, Setting = temperature.Setting };
            return await ExecuteCommandAsync(query, parameters);
        }
        public async Task<int> UpdateLocationAsync(Location location)
        {
            string tableName = Environment.GetEnvironmentVariable("Location_Table");
            string query = $"UPDATE {tableName} SET Setting = @Setting WHERE ID = @ID";
            var parameters = new { Setting = location.Setting, ID = location.ID };
            return await ExecuteCommandAsync(query, parameters);
        }
        public async Task<int> UpdateTemperatureAsync(Temperature temperature)
        {
            string tableName = Environment.GetEnvironmentVariable("Temperature_Table");
            string query = $"UPDATE {tableName} SET Value = @Value, Setting = @Setting WHERE ID = @ID";
            var parameters = new { Value = temperature.Value, Setting = temperature.Setting, ID = temperature.ID };
            return await ExecuteCommandAsync(query, parameters);
        }

    }
}
