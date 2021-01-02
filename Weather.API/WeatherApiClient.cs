using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Weather.Common;
using Weather.Common.Model;

namespace Weather.API
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private static double latitude = 32.7767;
        private static double longitude = -96.7970;

        public async Task<CurrentWeatherResponse> CurrentWeather()
        {
            var client = new RestClient($"https://weatherbit-v1-mashape.p.rapidapi.com/current?lon={longitude}&lat={latitude}&units=I");
            var request = new RestRequest(Method.GET);
            AddHeaders(request);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;
            var result = JsonConvert.DeserializeObject<ApiResponse<CurrentWeatherResponse>>(content).Data.FirstOrDefault();

            if (result == null)
                return null;
            
            result.Pressure = result.Pressure.ConvertToInchesMercury();
            return result;
        }

        public async Task<List<ForecastResponse>> Forecast()
        {
            var client = new RestClient($"https://weatherbit-v1-mashape.p.rapidapi.com/forecast/3hourly?lat={latitude}&lon={longitude}&units=I");
            var request = new RestRequest(Method.GET);
            AddHeaders(request);
            var response = await client.ExecuteAsync(request);
            var content = response.Content;
            var result = JsonConvert.DeserializeObject<ApiResponse<ForecastResponse>>(content).Data;
            return result;
        }

        private static void AddHeaders(RestRequest request)
        {
            request.AddHeader("x-rapidapi-key", "d577c7fd0emsh25f3602032f8f97p1b5eeajsn3fa888e88eaf");
            request.AddHeader("x-rapidapi-host", "weatherbit-v1-mashape.p.rapidapi.com");
        }
    }
}
