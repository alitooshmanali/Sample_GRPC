using Application.Aggregates.Users;
using Domain.Aggregates.Users;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Aggregates.Users
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private WriteDbContext _dbContext;

        public UserWriteRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(User user)
        {
            _dbContext.Users.Add(user);
        }

        public Task<User> GetByNationalCode(string nationalCode, CancellationToken cancellationToken = default) =>
            _dbContext.Users.FromSqlRaw(@"
                    SELECT      U.*
                    FROM        ""Users"" U
                    WHERE       U.""NationalCode"" = {0}
                ", nationalCode)
                .SingleOrDefaultAsync(cancellationToken);

        public void Remove(User user)
        {
            user.Delete();
            _dbContext.Users.Remove(user);
        }
    }
}
