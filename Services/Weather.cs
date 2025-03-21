using Test_API.Objects;
using Test_API.Repository;
using System.Linq;
using System.Threading.Tasks;
using Test_API.Controllers;
namespace Test_API.Services
{
    public class Weather : IWeather
    {
        private readonly IData _data;
        public Weather(IData Data)
        {
            _data = Data;
        }
        public async Task<Location> GetLocation(string location)
        {
            Location locationObj = null;
            IEnumerable<Location> locations = await _data.GetDataAsync<Location>(Environment.GetEnvironmentVariable("Location_Table"));

            locationObj = locations.Where(x => x.Setting == location).FirstOrDefault();

            return locationObj;
        }
        public async Task<Location> AddLocation(Location location)
        {
            string tableName = Environment.GetEnvironmentVariable("Location_Table");
            string query = $"INSERT INTO {tableName} (Setting) VALUES (@Setting) RETURNING ID;";

            var parameters = new { Setting = location.Setting };
            var insertedId = await _data.ExecuteCommandAsync(query, parameters);

            location.ID = insertedId;
            return location;
        }
    }
}
