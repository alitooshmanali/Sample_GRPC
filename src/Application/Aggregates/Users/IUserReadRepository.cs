using Application.Aggregates.Users.Queries;

namespace Application.Aggregates.Users
{
    public interface IUserReadRepository
    {
        IQueryable<UserQueryResult> GetAll();

        Task<UserQueryResult> GetByNationalCode(string nationalCode, CancellationToken cancellationToken = default);
    }
}
