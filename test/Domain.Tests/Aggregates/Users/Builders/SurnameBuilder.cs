using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Tests.Aggregates.Users.Builders
{
    public class SurnameBuilder
    {
        private string surname;
        public SurnameBuilder()
        {
            surname = "surname";
        }

        public SurnameBuilder WithSurname(string value)
        {
            surname = value;

            return this;
        }

        public Surname Build() => Surname.Create(surname);
    }
}
