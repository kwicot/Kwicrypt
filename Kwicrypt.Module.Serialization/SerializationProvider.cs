namespace Kwicrypt.Module.Serialization
{
    public static class SerializationProvider
    {
        public static ISerializationHandler Create(bool useJson)
        {
            if (useJson)
                return new JsonSerializationHandler();
            else
                return new MessagePackSerializationHandler();
        }
    }
}