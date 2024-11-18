
using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Aggregates.Users.Events
{
    public class BirthDayChangedEvent : DomainBaseEvent
    {
        public BirthDayChangedEvent(UserId id, BirthDay oldValue, BirthDay newValue)
            : base(id.Value)
        {
            OldValue = oldValue?.Value;
            NewValue = newValue?.Value;

        }

        public string? OldValue { get; }
        public string? NewValue { get; }
    }
}
