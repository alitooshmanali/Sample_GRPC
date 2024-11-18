

using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Aggregates.Users.Events
{
    public class UserCreatedEvent : DomainBaseEvent
    {
        public UserCreatedEvent(UserId id, Name name, Surname surname, NationalCode nationalCode) 
            : base(id.Value)
        {
            Name = name.Value;
            Surname = surname.Value;
            NationalCode = nationalCode.Value;
        }

        public string Name { get; }

        public string Surname { get; }

        public string NationalCode { get; }
    }
}
