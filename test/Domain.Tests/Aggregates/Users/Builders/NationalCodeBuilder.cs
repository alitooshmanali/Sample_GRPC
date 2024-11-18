using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Tests.Aggregates.Users.Builders
{
    public class NationalCodeBuilder
    {
        private string nationalCode;

        public NationalCodeBuilder()
        {
            nationalCode = "0123456789";
        }

        public NationalCodeBuilder WithNationalCode(string value)
        {
            nationalCode = value;

            return this;
        }

        public NationalCode Build() => NationalCode.Create(nationalCode);
    }
}
