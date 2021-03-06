﻿using Newtonsoft.Json;

namespace Weather.Common.Model
{
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

        [JsonProperty("pop")]
        public double ChanceOfPrecipitation { get; set; }
    }
}