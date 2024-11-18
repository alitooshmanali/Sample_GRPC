
using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Aggregates.Users.Events
{
    public class NameChangedEvent : DomainBaseEvent
    {
        public NameChangedEvent(UserId id, Name oldValue, Name newValue)
            : base(id.Value)
        {
            OldValue = oldValue?.Value;
            NewValue = newValue.Value;
        }

        public string? OldValue { get; }
        public string NewValue { get; }
    }
}
