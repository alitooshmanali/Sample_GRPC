using Domain.Aggregates.Users;

namespace Domain.Tests.Aggregates.Users.Builders
{
    public class UserBuilder
    {
        private Guid id;

        private string name;

        private string surname;

        private string nationalCode;


        public UserBuilder()
        {
            id = Guid.NewGuid();
            name = "Name";
            surname = "Surname";
            nationalCode = "0123456789";
        }

        public UserBuilder WithId(Guid value)
        {
            id = value;

            return this;
        }

        public UserBuilder WithName(string value)
        {
            name = value;

            return this;
        }

        public UserBuilder WithSurname(string value)
        {
            surname = value;

            return this;
        }

        public UserBuilder WithNationalCode(string value)
        {
            nationalCode = value;

            return this;
        }

        public User Build()
        {
            var idProperty = new UserIdBuilder().WithId(id).Build();
            var nameProperty = new NameBuilder().WithName(name).Build();
            var surnameProperty = new SurnameBuilder().WithSurname(surname).Build();
            var nationalCodeProperty = new NationalCodeBuilder().WithNationalCode(nationalCode).Build();



            var user = User.Create(idProperty, nameProperty, surnameProperty, nationalCodeProperty);

            return user;
        }
    }
}
