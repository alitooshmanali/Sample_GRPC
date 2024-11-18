using Domain.Properties;

namespace Domain.Aggregates.Users.ValueObjects.Rules
{
    internal class SurnameCannotBeEmptyRule : IBusinessRule
    {
        private readonly string value;

        public SurnameCannotBeEmptyRule(string value)
        {
            this.value = value;
        }

        public string Message => DomainResources.SurnameCannotBeEmpty;

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
