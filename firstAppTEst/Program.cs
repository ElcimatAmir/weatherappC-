using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace weatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "b0d75a0b5df9898bcc361437abcb5d1e";
            string countryName;
            
            Console.Write("enter a country name:  ");
            countryName = Console.ReadLine();

            string url = "http://api.openweathermap.org/data/2.5/weather?q="+countryName+ "&units=metric&appid=" + apiKey;
            string result;

            HttpWebRequest req  = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            WeatherResponse whatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(result);
           
            string sunrizeTime = ConvertFromUnixToDataTime(whatherResponse.Sys.sunrise).ToString("HH:mm");
            string sunsetTime = ConvertFromUnixToDataTime(whatherResponse.Sys.sunset).ToString("HH:mm");

            string resultTextForUsers = "temp in " + countryName + ": " + whatherResponse.Main.temp + " and humidity: %" + whatherResponse.Main.humidity + " sunrize time: "+sunrizeTime+" sunset time: "+sunsetTime;
            Console.WriteLine(resultTextForUsers);
            // далее для записи в файл  
            string path = @"../../../sechingHestory.txt";
            StreamWriter sw = new StreamWriter(path,true);
            sw.WriteLine(resultTextForUsers);
            sw.Close();

            
        }

        public static DateTime ConvertFromUnixToDataTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

    }
}
