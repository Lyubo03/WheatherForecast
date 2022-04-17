namespace WheatherForecast.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Services;
    using System;
    using System.Linq;
    using Models.DbModels;
    using Models.AuthModels;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using BC = BCrypt.Net.BCrypt;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService authenticationService;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
        {
            this.authenticationService = authenticationService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
                        .Where(x => x.ValidationState == ModelValidationState.Invalid)
                        .Select(x => x.Errors)
                        .Select(x => x.FirstOrDefault()?.ErrorMessage)
                        .FirstOrDefault();

                    return BadRequest(errorMessage);
                }

                var userValidation = await authenticationService.GetUserByUsernameAsync(model.Username);
                if (userValidation == null)
                {
                    return BadRequest("There is no user with such username!");
                }

                var result = BC.Verify(model.Password, userValidation.Password);
                if (!result)
                {
                    return BadRequest("Incorrect password!");
                }

                return Ok("User successfully logged in!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return this.StatusCode(500, $"Something went wrong - {ex.Message}!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
                        .Where(x => x.ValidationState == ModelValidationState.Invalid)
                        .Select(x => x.Errors)
                        .Select(x => x.FirstOrDefault()?.ErrorMessage)
                        .FirstOrDefault();

                    return BadRequest(errorMessage);
                }

                var userValidation = await authenticationService.GetUserByEmailAsync(user.Email);
                if (userValidation != null)
                {
                    return BadRequest("There is such user with such email already!");
                }

                userValidation = await authenticationService.GetUserByUsernameAsync(user.Username);
                if (userValidation != null)
                {
                    return BadRequest("There is such user with such username already!");
                }

                user.Id = Guid.NewGuid().ToString();
                user.Password = BC.HashPassword(user.Password);

                await authenticationService.CreateUserAsync(user);

                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return this.StatusCode(500, $"Something went wrong - {ex.Message}!");
            }
        }
    }
}