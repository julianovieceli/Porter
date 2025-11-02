using System.Text.Json; 

namespace Porter.Common.Utils;

public class JsonUtils
{
    public static T DeepClone<T>(T obj)
    {
        if (obj == null) return default(T);

        // 1. Serialize the object to a JSON string
        string jsonString = JsonSerializer.Serialize(obj);

        // 2. Deserialize the JSON string back into a new object of type T
        return JsonSerializer.Deserialize<T>(jsonString);
    }
}
