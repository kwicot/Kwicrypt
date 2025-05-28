using System.Text;
using System.Text.Json;

namespace Kwicrypt.Module.Serialization
{
    public class JsonSerializationHandler : ISerializationHandler
    {
        public byte[] Serialize<T>(T data)
        {
            var json = JsonSerializer.Serialize(data);
            return Encoding.UTF8.GetBytes(json);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<T>(json)!;
        }

        public string ContentType => "application/json";
    }
}