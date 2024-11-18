using Domain.Tests.Aggregates.Users.Builders;

namespace Domain.Tests.Aggregates.Users.ValuesObjects
{
    public class BirthDayTest
    {
        [Fact]
        public void TestCreate_WhenEverythingIsOk_PropertiesShouldHaveCorrectValues()
        {
            const string value = "1392/02/13";
            var birthDay = new BirthDayBuilder().WithBirthDay(value).Build();

            birthDay.Value.Should().Be(value);
        }

        [Theory]
        [InlineData("12/05/1392")]
        [InlineData("12/1392/05")]
        [InlineData("13/1395/21")]
        public void TestCreate_WhenValueNotCorrectFormat_ThrowException(string value)
        {
            var action = new Action(() => new BirthDayBuilder().WithBirthDay(value).Build());

            action.Should().Throw<DomainException>()
                .WithMessage(DomainResources.BirthDayMustBeValid);
        }
    }
}
