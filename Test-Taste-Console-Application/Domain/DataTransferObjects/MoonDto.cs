using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Test_Taste_Console_Application.Domain.DataTransferObjects.JsonObjects;

namespace Test_Taste_Console_Application.Domain.DataTransferObjects
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonPathConverter))]
    public class MoonDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("moon")]
        public string Moon
        {
            get => Id;
            set => Id = value;
        }

        [JsonProperty("rel")]
        public string Rel { get; set; }
        public string URLId => Rel.Split('/').Last();

        [JsonProperty("mass.massValue")]
        public float MassValue { get; set; }

        [JsonProperty("mass.massExponent")]
        public float MassExponent { get; set; }

        // ✅ New: Temperature from the API
        [JsonProperty("avgTemp")]
        public double? AvgTemp { get; set; }
    }
}
