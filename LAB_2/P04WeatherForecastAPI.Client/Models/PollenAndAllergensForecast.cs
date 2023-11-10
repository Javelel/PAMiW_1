using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Models
{
    public class PollenAndAllergensForecast
	{
    	public string Name { get; set; }
    	public int ID { get; set; }
    	public bool Ascending { get; set; }
    	public string LocalDateTime { get; set; }
    	public long EpochDateTime { get; set; }
    	public double Value { get; set; }
    	public string Category { get; set; }
    	public int CategoryValue { get; set; }
    	public string MobileLink { get; set; }
    	public string Link { get; set; }
	}
}