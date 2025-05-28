using MessagePack;

namespace Kwicrypt.Module.Serialization
{

    public class MessagePackSerializationHandler : ISerializationHandler
    {
        public byte[] Serialize<T>(T obj)
        {
            return MessagePackSerializer.Serialize(obj);
        }

        public T Deserialize<T>(byte[] data)
        {
            return MessagePackSerializer.Deserialize<T>(data);
        }

        public string ContentType => "application/x-msgpack";
    }
}