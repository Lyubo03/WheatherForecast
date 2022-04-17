namespace WheatherForecast.Models.ForecastModels
{
    public class Forecast
    {
        public int Date { get; set; }
        public string Weather { get; set; }
        public Temperature Temp2m { get; set; }
        public int Wind10m_Max { get; set; }
    }
}