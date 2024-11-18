namespace Domain
{
    public abstract class Entity : IAggregateRoot
    {
        private readonly List<IDomainEvent> _domains;

        private bool canBeDeleted;

        protected Entity()
        {
            _domains = new List<IDomainEvent>();
        }

        public IEnumerable<IDomainEvent> DomainEvents => _domains.AsReadOnly();

        public void ClearEvents() => _domains.Clear();


        public void AddEvent(IDomainEvent @event) => _domains.Add(@event);

        protected void MarkedAsDeleted() => canBeDeleted = true;

        public bool CanBeDeleted() => canBeDeleted;

        protected static void CheckRule(IBusinessRule rule)
        {
            if (!rule.IsBroken())
                return;

            throw new DomainException(rule.Message);
        }
    }
}
