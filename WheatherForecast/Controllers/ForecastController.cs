namespace WheatherForecast.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Services;
    using Models.ForecastModels;
    using Newtonsoft.Json.Linq;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ForecastController : ControllerBase
    {
        private readonly ICityService cityService;
        private readonly ILogger<ForecastController> logger;

        public ForecastController(ICityService cityService,
            ILogger<ForecastController> logger)
        {
            this.cityService = cityService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Cities()
        {
            try
            {
                var cities = await cityService.GetCityNamesAsync();
                return this.Ok(cities);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return this.StatusCode(500, $"Something went wrong - {ex.Message}!");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forecast>>> WeeklyForecast([FromQuery] string name)
        {
            try
            {
                var city = await cityService.GetCitiesByNameAsync(name);
                var weeklyForecast = new List<Forecast>();

                using (var client = new HttpClient())
                {
                    var baseUrl = $"https://www.7timer.info";
                    client.BaseAddress = new Uri(baseUrl);

                    client.DefaultRequestHeaders.Clear();

                    var Res = await client.GetAsync($"/bin/api.pl?lon=${city.Longitude}&lat={city.Latitude}&product=civillight&output=json");

                    if (Res.IsSuccessStatusCode)
                    {
                        var ObjResponse = await Res.Content.ReadAsStringAsync();
                        var data = $"[{ObjResponse.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries)[1]}]";
                        weeklyForecast = JsonConvert.DeserializeObject<List<Forecast>>(data).ToList();
                    }
                }
                return this.Ok(weeklyForecast);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return this.StatusCode(500, $"Something went wrong - {ex.Message}!");
            }
        }
    }
}