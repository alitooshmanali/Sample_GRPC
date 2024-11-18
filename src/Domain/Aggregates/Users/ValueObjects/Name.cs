using Domain.Aggregates.Users.ValueObjects.Rules;

namespace Domain.Aggregates.Users.ValueObjects
{
    public class Name : ValueObject
    {
        private Name() { }

        public string Value { get; private init; }

        public static Name Create(string value)
        {
            CheckRule(new NameCanNotBeEmptyRule(value));

            return new() { Value = value };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
