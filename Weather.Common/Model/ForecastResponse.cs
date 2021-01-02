using System;
using Newtonsoft.Json;

namespace Weather.Common.Model
{
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
}