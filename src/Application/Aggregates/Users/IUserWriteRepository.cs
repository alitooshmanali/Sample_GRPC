using Domain.Aggregates.Users;

namespace Application.Aggregates.Users
{
    public interface IUserWriteRepository
    {
        void Add(User user);

        Task<User> GetByNationalCode(string nationalCode, CancellationToken cancellationToken = default);

        void Remove(User user);
    }
}
