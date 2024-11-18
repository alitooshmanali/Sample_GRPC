using MediatR;

namespace Application.Aggregates.Users.Queries.GetUserCollections
{
    public class GetUserCollectionQuery : BaseCollectionQuery, IRequest<BaseCollectionResult<UserQueryResult>>
    {
    }
}
