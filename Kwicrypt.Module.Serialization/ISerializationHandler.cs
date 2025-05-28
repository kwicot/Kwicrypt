namespace Kwicrypt.Module.Serialization
{
    public interface ISerializationHandler
    {
        byte[] Serialize<T>(T data);
        T Deserialize<T>(byte[] bytes);
        string ContentType { get; }
    }
}