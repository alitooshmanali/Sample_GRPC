using Domain.Tests.Aggregates.Users.Builders;

namespace Domain.Tests.Aggregates.Users.ValuesObjects
{
    public class NameTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TestCreate_WhenValueIsEmpty_ThrowsException(string value)
        {
            var action = new Action(() => new NameBuilder().WithName(value).Build());

            action.Should().Throw<DomainException>().WithMessage(DomainResources.NameCanNotBeEmpty);
        }

        [Fact]
        public void TestCreate_WhenEverythingIsOk_PropertiesShouldHaveCorrectValues()
        {
            const string value = "Username";
            var username = new NameBuilder().WithName(value).Build();

            username.Value.Should().Be(value);
        }

        [Fact]
        public void TestEquality_WhenEverythingIsOk_MustBeTrue()
        {
            const string value = "Username";
            var first = new NameBuilder().WithName(value).Build();
            var second = new NameBuilder().WithName(value).Build();

            first.Should().Be(second);
        }
    }
}
