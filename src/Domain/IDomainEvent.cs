using MediatR;

namespace Domain
{
    public interface IDomainEvent: INotification
    {
        Guid AggregateId { get; }

        DateTimeOffset EventTime {  get; }

        Dictionary<string, object> Flatten();
    }
}