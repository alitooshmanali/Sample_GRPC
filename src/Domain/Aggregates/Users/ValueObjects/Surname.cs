

using Domain.Aggregates.Users.ValueObjects.Rules;

namespace Domain.Aggregates.Users.ValueObjects
{
    public class Surname : ValueObject
    {
        private Surname() { }

        public string Value { get; private init; }

        public static Surname Create(string value)
        {
            CheckRule(new SurnameCannotBeEmptyRule(value));

            return new() { Value = value };
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
