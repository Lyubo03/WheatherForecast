namespace WheatherForecast.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Reflection;

    using Data;
    using Data.Seeders;
    public static class ApplicationBuilderExtension
    {
        public static void UseDatabaseSeeding(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<WheatherForecastDbContext>())
                {
                    context.Database.Migrate();

                    var seeders = Assembly.GetAssembly(typeof(WheatherForecastDbContext))
                       .GetTypes()
                       .Where(type => typeof(ISeeder).IsAssignableFrom(type))
                       .Where(type => type.IsClass)
                       .Select(type => (ISeeder)scope.ServiceProvider.GetRequiredService(type))
                       .ToList();

                    foreach (var seeder in seeders)
                    {
                        seeder.SeedAsync().GetAwaiter().GetResult();
                    }
                }
            }
        }
    }
}