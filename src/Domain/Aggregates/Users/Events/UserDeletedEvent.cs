
using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Aggregates.Users.Events
{
    public class UserDeletedEvent : DomainBaseEvent
    {
        public UserDeletedEvent(UserId id) 
            : base(id.Value)
        {
        }
    }
}
