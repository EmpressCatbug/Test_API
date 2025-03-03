using Test_API.Objects;
using Test_API.Repository;
using System.Linq;
namespace Test_API.Services
{
    public class Weather
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
    }
}
