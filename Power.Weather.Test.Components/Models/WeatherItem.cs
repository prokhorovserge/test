using Newtonsoft.Json;

namespace Power.Weather.Test.Components.Models
{
    public class WeatherItem
    {
        [JsonProperty(Required = Required.Always)]
        public string Date { get; set; } = string.Empty; // last_updated / time

        [JsonProperty(Required = Required.Always)]
        public int Temperature { get; set; } // temp_c

        [JsonProperty(Required = Required.Always)]
        public string Condition { get; set; } = string.Empty; // condition.text

        [JsonProperty(Required = Required.Always)]
        public string ConditionIcon { get; set; } = string.Empty; //condition.icon

        [JsonProperty(Required = Required.Always)]
        public double WindSpeed { get; set; } //wind_kph

        [JsonProperty(Required = Required.Always)]
        public double Pressure { get; set; } //pressure_mb

        [JsonProperty(Required = Required.Always)]
        public double Humidity { get; set; } //humidity

        [JsonProperty(Required = Required.Always)]
        public double Cloudy { get; set; } //cloud
    }
}
