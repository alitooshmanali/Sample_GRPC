namespace Application.Aggregates.Audits
{
    public interface IAuditWriteRepository
    {
        void Add(Audit audit);
    }
}
