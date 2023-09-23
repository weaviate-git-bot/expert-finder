using MediatR;

namespace ExpertFinder.Shared;

public interface ICommand<TResponse>: IRequest<TResponse>
{
    
}

public interface ICommand: IRequest
{
}