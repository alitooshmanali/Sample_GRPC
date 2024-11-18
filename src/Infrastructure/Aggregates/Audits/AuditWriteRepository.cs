using Application.Aggregates.Audits;
using Infrastructure.Contexts;

namespace Infrastructure.Aggregates.Audits
{
    public class AuditWriteRepository : IAuditWriteRepository
    {
        private readonly WriteDbContext _dbContext;

        public AuditWriteRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Audit audit)
        {
            _dbContext.Audits.Add(audit);
        }
    }
}
