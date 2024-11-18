using Domain.Aggregates.Users.Events;
using Domain.Tests.Aggregates.Users.Builders;
using Domain.Tests.Helper;

namespace Domain.Tests.Aggregates.Users
{
    public class UserTest
    {
        [Fact]
        public void TestChangeName_WhenEverythingsIsOk_ValueMustBeSet()
        {
            // arrange
            const string name = "Ali";
            var userId = Guid.NewGuid();
            var expected = new NameBuilder().WithName(name).Build();
            var user = new UserBuilder()
                .WithId(userId)
                .Build();
            var oldValue = user.Name.Value;

            user.ClearEvents();

            // act
            user.ChangeName(expected);

            // assert
            var nameChangedEvent = user.AssertPublishedDomainEvent<NameChangedEvent>();

            nameChangedEvent.AggregateId.Should().Be(userId);
            nameChangedEvent.OldValue.Should().Be(oldValue);
            nameChangedEvent.NewValue.Should().Be(expected.Value);
            user.Name.Value.Should().Be(expected.Value);
        }
    }
}
