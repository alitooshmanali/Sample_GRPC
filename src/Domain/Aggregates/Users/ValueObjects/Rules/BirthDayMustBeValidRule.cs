using Domain.Properties;
using System.Globalization;

namespace Domain.Aggregates.Users.ValueObjects.Rules
{
    internal class BirthDayMustBeValidRule : IBusinessRule
    {
        private readonly string value;

        private const string format = "yyyy/MM/dd";

        public BirthDayMustBeValidRule(string value)
        {
            this.value = value;
        }

        public string Message => DomainResources.BirthDayMustBeValid;

        public bool IsBroken()
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            DateTime dateValue;

            return !DateTime.TryParseExact(value,
                                          format,
                                          new CultureInfo("fa-IR"),
                                          DateTimeStyles.None,
                                          out dateValue);
        }
    }
}
