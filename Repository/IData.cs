
using Test_API.Objects;

namespace Test_API.Repository
{
    public interface IData
    {
        Task<int> ExecuteCommandAsync(string query, object parameters = null);
        Task<IEnumerable<T>> GetDataAsync<T>(string tableName);
        Task<int> AddLocationAsync(Location location);
        Task<int> AddTemperatureAsync(Temperature temperature);
        Task<int> UpdateLocationAsync(Location location);
        Task<int> UpdateTemperatureAsync(Temperature temperature);

    }
}