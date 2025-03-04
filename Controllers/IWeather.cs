using Test_API.Objects;

namespace Test_API.Controllers
{
    internal interface IWeather
    {
        Task<Location> GetLocation(string location);
    }
}