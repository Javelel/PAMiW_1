using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Models
{
    internal class DailyForecast
{
    public string Date { get; set; }
    public long EpochDate { get; set; }
    public DailyTemperature Temperature { get; set; }
    public DayNightInfo Day { get; set; }
    public DayNightInfo Night { get; set; }
    public List<string> Sources { get; set; }
    public string MobileLink { get; set; }
    public string Link { get; set; }
}
}