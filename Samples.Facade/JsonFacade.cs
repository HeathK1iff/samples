using System.Text.Json;

namespace Samples.Facade
{
    public abstract class JsonFacade
    {
        public static string ToJson<T>(T obj)
        {
           return JsonSerializer.Serialize<T>(obj);
        }

        public static T? ToObject<T>(string jsonData)
        {
            return (T?) JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}