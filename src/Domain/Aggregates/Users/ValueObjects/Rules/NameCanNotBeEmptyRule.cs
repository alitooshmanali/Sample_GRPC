using Domain.Properties;

namespace Domain.Aggregates.Users.ValueObjects.Rules
{
    internal class NameCanNotBeEmptyRule : IBusinessRule
    {
        private readonly string value;

        public NameCanNotBeEmptyRule(string value)
        {
            this.value = value;
        }
        public string Message => DomainResources.NameCanNotBeEmpty;

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
