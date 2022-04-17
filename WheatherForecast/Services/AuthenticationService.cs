namespace WheatherForecast.Services
{
    using Data;
    using System.Linq;
    using System.Threading.Tasks;
    using WheatherForecast.Models.DbModels;

    public class AuthenticationService : IAuthenticationService
    {
        private readonly WheatherForecastDbContext context;

        public AuthenticationService(WheatherForecastDbContext context)
        {
            this.context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
                => context.Users.FirstOrDefault(x => x.Email == email);

        public async Task<User> GetUserByUsernameAsync(string username)
                => context.Users.FirstOrDefault(x => x.Username == username);
    }
}