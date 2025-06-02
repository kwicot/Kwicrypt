namespace Core
{
    public class WebRequestResult<T>
    {
        public long Code { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

        public bool Success => Code == 200;
    }
}