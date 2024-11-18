
using Domain.Aggregates.Users.ValueObjects.Rules;

namespace Domain.Aggregates.Users.ValueObjects
{
    public class BirthDay : ValueObject
    {
        private BirthDay() { }

        public string? Value { get; private init; }

        public static BirthDay Create(string value)
        {
            CheckRule(new BirthDayMustBeValidRule(value));

            return new() { Value = value };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value!;
        }
    }
}
