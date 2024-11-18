using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Tests.Aggregates.Users.Builders
{
    public class BirthDayBuilder
    {
        private string birthday;

        public BirthDayBuilder()
        {
            birthday = "1392/02/13";
        }

        public BirthDayBuilder WithBirthDay(string birthday)
        {
            this.birthday = birthday;

            return this;
        }

        public BirthDay Build() => BirthDay.Create(birthday);
    }
}
