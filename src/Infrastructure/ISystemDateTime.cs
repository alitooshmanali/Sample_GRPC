namespace Infrastructure
{
    public interface ISystemDateTime
    {
        DateTimeOffset UtcNow { get; }
    }
}
