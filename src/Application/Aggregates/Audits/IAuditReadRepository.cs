using Application.Aggregates.Audits.Queries;

namespace Application.Aggregates.Audits
{
    public interface IAuditReadRepository
    {
        IQueryable<AuditQueryResult> GetAll();
    }
}
