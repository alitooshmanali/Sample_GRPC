
using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Aggregates.Users.Events
{
    public class SurnameChangedEvent : DomainBaseEvent
    {
        public SurnameChangedEvent(UserId id, Surname oldValue, Surname newValue)
            : base(id.Value)
        {
            OldValue = oldValue?.Value;
            NewValue = newValue.Value;
        }

        public string? OldValue { get; }
        public string NewValue { get; }
    }
}
