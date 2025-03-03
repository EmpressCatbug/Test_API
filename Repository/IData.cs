
namespace Test_API.Repository
{
    public interface IData
    {
        Task<int> ExecuteCommandAsync(string query, object parameters = null);
        Task<IEnumerable<T>> GetDataAsync<T>(string tableName);
    }
}