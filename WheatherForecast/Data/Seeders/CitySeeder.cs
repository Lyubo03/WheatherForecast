namespace WheatherForecast.Data.Seeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Models.DbModels;

    public class CitySeeder : ISeeder
    {
        private readonly WheatherForecastDbContext context;

        public CitySeeder(WheatherForecastDbContext context)
        {
            this.context = context;
        }
        public async Task SeedAsync()
        {
            if (!context.Cities.Any())
            {
                await context.AddAsync(new City
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Petrich",
                    Longitude = "23.206685",
                    Latitude = "41.398108"
                });

                await context.AddAsync(new City
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Blagoevgrad",
                    Longitude = "23.249999",
                    Latitude = "41.749997"
                });

                await context.AddAsync(new City
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Burgas",
                    Longitude = "27.462636",
                    Latitude = "42.504793"
                });

                await context.AddAsync(new City
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Varna",
                    Longitude = "27.914733",
                    Latitude = "43.214050"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}