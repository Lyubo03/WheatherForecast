namespace WheatherForecast.Services
{
    using System.Threading.Tasks;
    using WheatherForecast.Models.DbModels;

    public interface IAuthenticationService
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUsernameAsync(string username);
    }
}