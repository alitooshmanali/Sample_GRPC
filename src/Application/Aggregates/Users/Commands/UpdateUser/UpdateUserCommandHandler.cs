using Application.Properties;
using Domain;
using Domain.Aggregates.Users.ValueObjects;
using MediatR;

namespace Application.Aggregates.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserWriteRepository userWriteRepository;

        public UpdateUserCommandHandler(IUserWriteRepository userWriteRepository)
        {
            this.userWriteRepository = userWriteRepository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userWriteRepository
                .GetByNationalCode(request.NationalCode)
                .ConfigureAwait(false) ??
                throw new DomainException(string.Format(ApplicationResources.Global_UnableToFound, request.NationalCode));


            user.ChangeName(Name.Create(request.Name));
            user.ChangeSurname(Surname.Create(request.Surname));
            user.ChangeBirthDay(BirthDay.Create(request.BirthDay));
        }
    }
}
