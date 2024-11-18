using Application.Aggregates.Users;
using Application.Aggregates.Users.Queries;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Aggregates.Users
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly WriteDbContext _dbContext;

        public UserReadRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<UserQueryResult> GetAll()
        {
            return _dbContext.Database.SqlQueryRaw<UserQueryResult>($@"
                    SELECT      U.""Id"",
                                U.""Name"",
                                U.""Surname"",
                                U.""NationalCode"",
                                U.""BirthDay"",
                                U.""Created"",
                                U.""Updated"",
                                U.""Version""
                    FROM        ""Users"" AS U
                ");
        }

        public async Task<UserQueryResult> GetByNationalCode(string nationalCode, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Database.SqlQueryRaw<UserQueryResult>($@"
                    SELECT      U.""Id"",
                                U.""Name"",
                                U.""Surname"",
                                U.""NationalCode"",
                                U.""BirthDay"",
                                U.""Created"",
                                U.""Updated"",
                                U.""Version""
                    FROM        ""Users"" AS U
                    WHERE       U.""NationalCode"" = {{0}}
                ", nationalCode)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

        }
    }
}
