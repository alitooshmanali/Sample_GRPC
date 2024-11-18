namespace Grpc.SDK.Models
{
    public class Response<T>
    {
        public T Value { get; set; }

        public string Message { get; set; }
    }

    public class Response
    {
        public string Message { get; set; }
    }
}
