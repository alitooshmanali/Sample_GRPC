
using Domain.Aggregates.Users.ValueObjects.Rules;

namespace Domain.Aggregates.Users.ValueObjects
{
    public class NationalCode : ValueObject
    {
        private NationalCode() { }

        public string Value { get; private init; }

        public static NationalCode Create(string value)
        {
            CheckRule(new NationalCodeCannotBeEmptyRule(value));
            CheckRule(new NationalCodeMustHaveTenNumberRule(value));

            return new() { Value = value };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
