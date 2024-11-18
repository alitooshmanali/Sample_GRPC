using MediatR;

namespace Application.Aggregates.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string NationalCode { get; set; }

        public string BirthDay { get; set; }
    }
}
