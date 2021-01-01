using System.Collections.Generic;
using Newtonsoft.Json;

namespace Weather.API
{
    public class ApiResponse<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}