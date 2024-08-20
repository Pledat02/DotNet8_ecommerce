using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Helper
{
    public static class SessionExtensions
    {
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            // Bảo tồn tham chiếu đối tượng để xử lý các tham chiếu vòng lặp
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true // Tùy chọn: Làm cho đầu ra JSON dễ đọc hơn
        };

        public static void Set<T>(this ISession session, string key, T value)
        {
            var jsonString = JsonSerializer.Serialize(value, _serializerOptions);
            session.SetString(key, jsonString);
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var jsonString = session.GetString(key);
            return jsonString == null ? default : JsonSerializer.Deserialize<T>(jsonString, _serializerOptions);
        }
    }
}
