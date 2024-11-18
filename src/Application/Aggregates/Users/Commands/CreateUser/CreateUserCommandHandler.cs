using Domain.Aggregates.Users;
using Domain.Aggregates.Users.ValueObjects;
using MediatR;

namespace Application.Aggregates.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserWriteRepository userWriteRepository;

        public CreateUserCommandHandler(IUserWriteRepository userWriteRepository)
        {
            this.userWriteRepository = userWriteRepository;
        }


        public Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();
            var user = User.Create(
                UserId.Create(userId),
                Name.Create(request.Name),
                Surname.Create(request.Surname),
                NationalCode.Create(request.NationalCode));

            user.ChangeBirthDay(BirthDay.Create(request.BirthDay));

            userWriteRepository.Add(user);

            return Task.FromResult(request.NationalCode);
        }
    }
}
