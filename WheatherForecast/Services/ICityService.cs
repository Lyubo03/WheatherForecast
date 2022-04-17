namespace WheatherForecast.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models.DbModels;

    public interface ICityService
    {
        Task<IQueryable<string>> GetCityNamesAsync();
        Task<City> GetCitiesByNameAsync(string name);
    }
}