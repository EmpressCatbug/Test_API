using Test_API.Objects;

namespace Test_API.Controllers
{
    public interface IWeather
    {
        Task<Location> GetLocation(string location);
    }
}