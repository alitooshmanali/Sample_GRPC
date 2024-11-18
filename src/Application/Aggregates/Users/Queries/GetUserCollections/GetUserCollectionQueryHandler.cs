using MediatR;

namespace Application.Aggregates.Users.Queries.GetUserCollections
{
    public class GetUserCollectionQueryHandler :
        IRequestHandler<GetUserCollectionQuery, BaseCollectionResult<UserQueryResult>>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetUserCollectionQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public Task<BaseCollectionResult<UserQueryResult>> Handle(GetUserCollectionQuery request, CancellationToken cancellationToken)
        {
            var source = _userReadRepository.GetAll().OrderBy(i => i.Id);
            var results = source
                .ApplyPagination(request.PageIndex, request.PageSize)
                .ToArray();

            return Task.FromResult(new BaseCollectionResult<UserQueryResult>
            {
                Result = results,
                TotalCount = results.Count()
            });
        }
    }
}
