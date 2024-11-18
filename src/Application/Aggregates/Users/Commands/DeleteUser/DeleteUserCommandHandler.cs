using Application.Properties;
using Domain;
using MediatR;

namespace Application.Aggregates.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserWriteRepository userWriteRepository;

        public DeleteUserCommandHandler(IUserWriteRepository userWriteRepository)
        {
            this.userWriteRepository = userWriteRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userWriteRepository.GetByNationalCode(request.NationalCode).ConfigureAwait(false)
                ?? throw new DomainException(string.Format(ApplicationResources.Global_UnableToFound, request.NationalCode));

            userWriteRepository.Remove(user);
        }
    }
}
