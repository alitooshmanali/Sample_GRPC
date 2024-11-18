using Domain.Tests.Aggregates.Users.Builders;

namespace Domain.Tests.Aggregates.Users.ValuesObjects
{
    public class NationalCodeTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TestCreate_WhenValueIsEmpty_ThrowsException(string value)
        {
            var action = new Action(() => new NationalCodeBuilder().WithNationalCode(value).Build());

            action.Should().Throw<DomainException>().WithMessage(DomainResources.NationalCodeCannotBeEmpty);
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("0012345")]
        public void TestCreate_WhenValueLessThanTen_ThrowsException(string value)
        {
            var action = new Action(() => new NationalCodeBuilder().WithNationalCode(value).Build());

            action.Should().Throw<DomainException>().WithMessage(DomainResources.NationalCodeMustHaveTenNumber);
        }

        [Fact]
        public void TestCreate_WhenEverythingIsOk_PropertiesShouldHaveCorrectValues()
        {
            const string value = "0014235695";
            var nationalCode = new NationalCodeBuilder().WithNationalCode(value).Build();

            nationalCode.Value.Should().Be(value);
        }

        [Fact]
        public void TestEquality_WhenEverythingIsOk_MustBeTrue()
        {
            const string value = "0014235695";
            var first = new NationalCodeBuilder().WithNationalCode(value).Build();
            var second = new NationalCodeBuilder().WithNationalCode(value).Build();

            first.Should().Be(second);
        }
    }
}
