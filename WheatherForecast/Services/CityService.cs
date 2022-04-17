namespace WheatherForecast.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    
    using Data;
    using Models.DbModels;

    public class CityService : ICityService
    {
        private readonly WheatherForecastDbContext context;

        public CityService(WheatherForecastDbContext context)
        {
            this.context = context;
        }

        public async Task<IQueryable<string>> GetCityNamesAsync()
            => context.Cities.Select(x => x.Name);

        public async Task<City> GetCitiesByNameAsync(string name)
            => context.Cities.FirstOrDefault(x => x.Name == name);
    }
}