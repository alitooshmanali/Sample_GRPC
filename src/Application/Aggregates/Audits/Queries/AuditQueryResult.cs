namespace Application.Aggregates.Audits.Queries
{
    public class AuditQueryResult
    {
        public Guid Id { get; set; }

        public Guid AggregateId { get; set; }

        public string User { get; set; }

        public string Action { get; set; }

        public string Data { get; set; }

        public string Client { get; set; }

        public string ClientAddress { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
