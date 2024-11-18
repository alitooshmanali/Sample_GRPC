using Application.Aggregates.Audits;
using Application.Aggregates.Audits.Queries;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Aggregates.Audits
{
    public class AuditReadRepository : IAuditReadRepository
    {
        private readonly WriteDbContext _dbContext;

        public AuditReadRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<AuditQueryResult> GetAll()
        {
            return _dbContext.Database.SqlQueryRaw<AuditQueryResult>($@"
                    SELECT      A.""Id"",
                                A.""AggregateId"",                                
                                COALESCE(U.""Username"", A.""UserId""::TEXT) AS ""User"",                                
                                A.""Action"",
                                A.""Data"",
                                A.""Time""
                    FROM        ""Audits"" AS A
                    LEFT JOIN   ""Users""       AS U   ON A.""UserId""      = U.""Id""
                ");
        }
    }
}
