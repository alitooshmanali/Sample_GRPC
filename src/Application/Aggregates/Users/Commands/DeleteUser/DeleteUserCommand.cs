using MediatR;

namespace Application.Aggregates.Users.Commands.DeleteUser
{
    public class DeleteUserCommand: IRequest
    {
        public string NationalCode { get; set; }
    }
}
