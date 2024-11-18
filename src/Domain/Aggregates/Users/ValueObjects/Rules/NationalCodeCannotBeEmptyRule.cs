using Domain.Properties;

namespace Domain.Aggregates.Users.ValueObjects.Rules
{
    internal class NationalCodeCannotBeEmptyRule : IBusinessRule
    {
        private readonly string value;

        public NationalCodeCannotBeEmptyRule(string value)
        {
            this.value = value;
        }

        public string Message => DomainResources.NationalCodeCannotBeEmpty;

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
