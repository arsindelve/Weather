using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace Weather.API
{

    public class ApiResponse<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }

    public class CurrentWeatherResponse
    {
        [JsonProperty("precip")]
        public double Precipitation { get; set; }

        [JsonProperty("wind_spd")]
        public double WindSpeed { get; set; }

        [JsonProperty("pres")]
        public double Pressure { get; set; }

        public double Clouds { get; set; }

        [JsonProperty("temp")]
        public double Temperature { get; set; }
    }

    public class ForecastResponse : CurrentWeatherResponse
    {
        [JsonProperty("pop")]
        public double ChanceOfPrecipitation { get; set; }

        [JsonProperty("timestamp_utc")]
        public DateTime TimeUniversal { get; set; }

        public DateTime ForecastTime
        {
            get
            {
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(TimeUniversal, cstZone);
            }
        }
    }

    public interface IWeatherApiClient
    {
        void CurrentWeather();
        void Forecast();
    }

    public class WeatherApiClient : IWeatherApiClient
    {
        private static double latitude = 32.745499;
        private static double longitude = -97.003532;

        public void CurrentWeather()
        {
            var client = new RestClient($"https://weatherbit-v1-mashape.p.rapidapi.com/current?lon={longitude}&lat={latitude}&units=I");
            var request = new RestRequest(Method.GET);
            AddHeaders(request);
            var response = client.Execute(request);
            var content = response.Content;
            var weather = JsonConvert.DeserializeObject<ApiResponse<CurrentWeatherResponse>>(content);
        }

        public void Forecast()
        {
            var client = new RestClient($"https://weatherbit-v1-mashape.p.rapidapi.com/forecast/3hourly?lat={latitude}&lon={longitude}&units=I");
            var request = new RestRequest(Method.GET);
            AddHeaders(request);
            var response = client.Execute(request);
            var content = response.Content;
            var weather = JsonConvert.DeserializeObject<ApiResponse<ForecastResponse>>(content);

        }

        private static void AddHeaders(RestRequest request)
        {
            request.AddHeader("x-rapidapi-key", "d577c7fd0emsh25f3602032f8f97p1b5eeajsn3fa888e88eaf");
            request.AddHeader("x-rapidapi-host", "weatherbit-v1-mashape.p.rapidapi.com");
        }
    }
}
