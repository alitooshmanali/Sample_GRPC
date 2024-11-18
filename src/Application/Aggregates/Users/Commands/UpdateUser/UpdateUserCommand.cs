using MediatR;

namespace Application.Aggregates.Users.Commands.UpdateUser
{
    public class UpdateUserCommand: IRequest
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string NationalCode { get; set; }

        public string BirthDay { get; set; }
    }
}
