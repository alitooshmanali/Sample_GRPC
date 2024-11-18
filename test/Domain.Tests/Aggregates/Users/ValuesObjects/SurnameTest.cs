using Domain.Tests.Aggregates.Users.Builders;

namespace Domain.Tests.Aggregates.Users.ValuesObjects
{
    public class SurnameTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TestCreate_WhenValueIsEmpty_ThrowsException(string value)
        {
            var action = new Action(() => new SurnameBuilder().WithSurname(value).Build());

            action.Should().Throw<DomainException>().WithMessage(DomainResources.SurnameCannotBeEmpty);
        }

        [Fact]
        public void TestCreate_WhenEverythingIsOk_PropertiesShouldHaveCorrectValues()
        {
            const string value = "Lastname";
            var surname = new SurnameBuilder().WithSurname(value).Build();

            surname.Value.Should().Be(value);
        }

        [Fact]
        public void TestEquality_WhenEverythingIsOk_MustBeTrue()
        {
            const string value = "Lastname";
            var first = new SurnameBuilder().WithSurname(value).Build();
            var second = new SurnameBuilder().WithSurname(value).Build();

            first.Should().Be(second);
        }
    }
}
