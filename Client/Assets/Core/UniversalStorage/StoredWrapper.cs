using System;

namespace Core.UniversalStorage
{
    [Serializable]
    public class StoredWrapper <T>
    {
        public T Data;
        public long Timestamp;
        public int? LiveMinutes;
    }
}