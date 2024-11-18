using Domain.Properties;

namespace Domain.Aggregates.Users.ValueObjects.Rules
{
    internal class UserIdCanNotBeEmptyRule : IBusinessRule
    {
        private readonly Guid value;

        public UserIdCanNotBeEmptyRule(Guid value)
        {
            this.value = value;
        }

        public string Message => DomainResources.IdCannotBeEmpty;

        public bool IsBroken()
        {
            return Guid.Empty == value;
        }
    }
}
