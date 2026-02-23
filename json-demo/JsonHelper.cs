using System.Text.Json;
using System.Text.Json.Serialization;

namespace json_demo
{
    public static class JsonHelper
    {
        public static string ToJson(object obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(obj, options);
        }
    }
}