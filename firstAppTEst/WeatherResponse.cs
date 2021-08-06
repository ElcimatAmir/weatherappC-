using System;


namespace weatherApp
{
	public class WeatherResponse
	{
		public TempInfo Main{ get; set; }
		public Sys Sys { get; set; }
	}
}
