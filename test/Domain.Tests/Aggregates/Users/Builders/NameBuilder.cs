using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Tests.Aggregates.Users.Builders
{
    public class NameBuilder
    {
        private string name;
        public NameBuilder()
        {
            name = "name";
        }

        public NameBuilder WithName(string value)
        {
            name = value;

            return this;
        }

        public Name Build() => Name.Create(name);
    }
}
