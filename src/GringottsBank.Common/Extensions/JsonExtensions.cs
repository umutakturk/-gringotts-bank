using System.Text.Json;

namespace GringottsBank.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object @object)
            => JsonSerializer.Serialize(@object);

        public static T FromJson<T>(this string json)
            => JsonSerializer.Deserialize<T>(json);
    }
}
