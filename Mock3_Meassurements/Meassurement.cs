using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mock3_Meassurements
{
	public class Meassurement
	{
		public int Id { get; set; }
		public string Pressure { get; set; }
		public string Humidity { get; set; }
		public string Temperature { get; set; }
		public DateTime TimeStamp { get; set; }

		public Meassurement(int id, string pressure, string humidity, string temperature, DateTime timeStamp)
		{
			Id = id;
			Pressure = pressure;
			Humidity = humidity;
			Temperature = temperature;
			TimeStamp = timeStamp;
		}

		public Meassurement()
		{
			
		}
	}
}
