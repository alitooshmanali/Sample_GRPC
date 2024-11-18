using Domain.Aggregates.Users.ValueObjects;

namespace Domain.Tests.Aggregates.Users.Builders
{
    public class UserIdBuilder
    {
        private Guid id;

        public UserIdBuilder()
        {
            id = Guid.NewGuid();
        }

        public UserIdBuilder WithId(Guid value)
        {
            id = value;

            return this;
        }

        public UserId Build() => UserId.Create(id);
    }
}
