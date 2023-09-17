using MediatR;

namespace RailFlow.Infrastructure.DAL.Decorators;

internal sealed class UnitOfWorkDecorator<TRequest> : IRequestHandler<TRequest> where TRequest : class, IRequest
{
    private readonly IRequestHandler<TRequest> _requestHandler;
    private readonly IUnitOfWork _unitOfWork;
    
    public UnitOfWorkDecorator(IRequestHandler<TRequest> requestHandler, IUnitOfWork unitOfWork)
    {
        _requestHandler = requestHandler;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        await _unitOfWork.ExecuteAsync(() => _requestHandler.Handle(request, cancellationToken));
    }
}