using System.Text.Json.Serialization;

namespace CalculationAPI.Model
{
    public class CalculateResponse
    {
        [JsonPropertyName("success")]
        public bool Success {  get; set; }
        [JsonPropertyName("data")]
        public object? Data { get; set; }
    }
}
