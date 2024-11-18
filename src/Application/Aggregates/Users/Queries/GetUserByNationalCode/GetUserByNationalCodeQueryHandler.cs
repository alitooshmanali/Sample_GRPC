using Application.Aggregates.Users.Queries.GetUserByNationalCode;
using MediatR;

namespace Application.Aggregates.Users.Queries.GetUserByUsername
{
    public class GetUserByNationalCodeQueryHandler : IRequestHandler<GetUserByNationalCodeQuery, UserQueryResult>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetUserByNationalCodeQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public Task<UserQueryResult> Handle(GetUserByNationalCodeQuery request, CancellationToken cancellationToken)
            => _userReadRepository.GetByNationalCode(request.NationalCode, cancellationToken);
    }
}
