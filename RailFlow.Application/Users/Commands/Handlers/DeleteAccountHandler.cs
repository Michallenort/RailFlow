using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;
using Railflow.Core.Services;

namespace RailFlow.Application.Users.Commands.Handlers;

internal sealed class DeleteAccountHandler : IRequestHandler<DeleteAccount>
{
    private readonly IUserRepository _userRepository;
    private readonly IContextService _contextService;
    
    public DeleteAccountHandler(IUserRepository userRepository, IContextService contextService)
    {
        _userRepository = userRepository;
        _contextService = contextService;
    }
    
    public async Task Handle(DeleteAccount request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(_contextService.UserId);
        
        if (user is null)
        {
            throw new UserNotFoundException(_contextService.UserId);
        }
        
        await _userRepository.DeleteAsync(user);
    }
}