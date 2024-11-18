using Domain.Properties;

namespace Domain.Aggregates.Users.ValueObjects.Rules
{
    internal class NationalCodeMustHaveTenNumberRule : IBusinessRule
    {
        private readonly string value;

        public NationalCodeMustHaveTenNumberRule(string value)
        {
            this.value = value;
        }

        public string Message => DomainResources.NationalCodeMustHaveTenNumber;

        public bool IsBroken()
        {
            return value.Length != 10;
        }
    }
}
