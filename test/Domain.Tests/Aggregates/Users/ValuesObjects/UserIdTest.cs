using Domain.Tests.Aggregates.Users.Builders;

namespace Domain.Tests.Aggregates.Users.ValuesObjects
{
    public class UserIdTest
    {
        [Fact]
        public void TestCreate_WhenEverythingIsOk_PropertiesShouldHaveCorrectValues()
        {
            var value = Guid.NewGuid();
            var userId = new UserIdBuilder().WithId(value).Build();

            userId.Value.Should().Be(value);
        }

        [Theory]
        [InlineData("37ABBF87-A96D-4593-A0C4-23FEC62D6559", true)]
        [InlineData("CD8BE2BC-982D-4258-9A2F-3AE3D967AA76", false)]
        public void TestEquality_WhenEverythingIsOk_ResultMustBeExpected(string value, bool result)
        {
            var first = new UserIdBuilder().WithId(Guid.Parse("37ABBF87-A96D-4593-A0C4-23FEC62D6559")).Build();
            var second = new UserIdBuilder().WithId(Guid.Parse(value)).Build();

            first.Equals(second).Should().Be(result);
        }
    }
}
