using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fusionary.UnitTesting;

public static class JsonHelper {
    public static JsonSerializerOptions CreateOptions()
    {
        JsonSerializerOptions options = new() {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

        return options;
    }
}
