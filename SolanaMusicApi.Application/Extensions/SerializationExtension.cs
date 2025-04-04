using Newtonsoft.Json;

namespace SolanaMusicApi.Application.Extensions;

public static class SerializationExtension
{
    public static string Serialize<T>(this T obj)
    {
        try
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        catch (JsonException ex)
        {
            throw new JsonException($"Serialization error - {ex.Message} for object: {obj}");
        }
    }

    public static T? Deserialize<T>(this string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (JsonException ex)
        {
            throw new JsonException($"Deserialization error - {ex.Message} in\n{json}");
        }
    }
}
