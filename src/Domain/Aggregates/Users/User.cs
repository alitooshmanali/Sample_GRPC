
using Domain.Aggregates.Users.Events;
using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Aggregates.Users
{
    public class User : Entity
    {
        private User() { }

        public UserId Id { get; private set; }

        public Name Name { get; private set; }

        public Surname Surname { get; private set; }

        public NationalCode NationalCode { get; private set; }

        public BirthDay BirthDay { get; private set; }

        public static User Create(UserId id,
            Name name,
            Surname surname,
            NationalCode nationalCode)
        {
            var user = new User
            {
                Id = id,
                Name = name,
                Surname = surname,
                NationalCode = nationalCode
            };

            user.AddEvent(new UserCreatedEvent(id, name, surname, nationalCode));

            return user;
        }

        public void ChangeName(Name value)
        {
            if (Name == value) return;

            AddEvent(new NameChangedEvent(Id, Name, value));

            Name = value;
        }

        public void ChangeSurname(Surname value)
        {
            if (Surname == value) return;

            AddEvent(new SurnameChangedEvent(Id, Surname, value));

            Surname = value;
        }

        public void ChangeBirthDay(BirthDay value)
        {
            if(BirthDay == value)
                return;

            AddEvent(new BirthDayChangedEvent(Id, BirthDay, value));

            BirthDay = value;
        }

        public void Delete()
        {
            if (CanBeDeleted())
                throw new InvalidOperationException();

            AddEvent(new UserDeletedEvent(Id));
            MarkedAsDeleted();
        }
    }


}
