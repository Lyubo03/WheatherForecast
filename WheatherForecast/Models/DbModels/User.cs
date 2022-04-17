namespace WheatherForecast.Models.DbModels
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}