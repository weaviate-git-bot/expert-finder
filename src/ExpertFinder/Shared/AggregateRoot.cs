namespace ExpertFinder.Shared;

public abstract class AggregateRoot
{
    private readonly List<IDomainEvent> _pendingDomainEvents = new();
    
    public IReadOnlyList<IDomainEvent> PendingDomainEvents => _pendingDomainEvents.AsReadOnly();

    protected void EmitDomainEvent(IDomainEvent evt)
    {
        if (TryApplyDomainEvent(evt))
        {
            _pendingDomainEvents.Add(evt);
        }
    }

    protected abstract bool TryApplyDomainEvent(IDomainEvent evt);
}