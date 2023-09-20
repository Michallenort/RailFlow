using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;
using Railflow.Core.Services;

namespace RailFlow.Application.Users.Commands.Handlers;

internal sealed class UpdateAccountHandler : IRequestHandler<UpdateAccount> 
{
    private readonly IUserRepository _userRepository;
    private readonly IContextService _contextService;
    private readonly IValidator<UpdateAccount> _validator;
    
    public UpdateAccountHandler(IUserRepository userRepository, IContextService contextService, 
        IValidator<UpdateAccount> validator)
    {
        _userRepository = userRepository;
        _contextService = contextService;
        _validator = validator;
    }
    
    public async Task Handle(UpdateAccount request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var user = await _userRepository.GetByIdAsync(_contextService.UserId);
        
        if (user is null)
        {
            throw new UserNotFoundException(_contextService.UserId);
        }
        
        user.Update(request.Email, request.FirstName, request.LastName, request.DateOfBirth);
        
        await _userRepository.UpdateAsync(user);
    }
}