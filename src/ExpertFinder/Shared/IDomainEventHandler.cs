using MediatR;

namespace ExpertFinder.Shared;

public interface IDomainEventHandler<TDomainEvent>: INotificationHandler<TDomainEvent> where TDomainEvent: IDomainEvent
{
    
}