namespace WheatherForecast.Data
{
    using Microsoft.EntityFrameworkCore;
    using WheatherForecast.Models.DbModels;

    public class WheatherForecastDbContext : DbContext
    {
        public WheatherForecastDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}