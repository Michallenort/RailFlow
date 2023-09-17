using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using RailFlow.Application.Security;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Users.Commands.Handlers;

public class SignUpHandler : IRequestHandler<SignUp>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IValidator<SignUp> _validator;
    
    public SignUpHandler(IUserRepository userRepository, IPasswordManager passwordManager, IValidator<SignUp> validator)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _validator = validator;
    }
    
    public async Task Handle(SignUp request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        if (await _userRepository.GetByEmailAsync(request.Email) is not null)
        {
            throw new EmailExistsException(request.Email);
        }
        
        var securedPassword = _passwordManager.Secure(request.Password);

        var user = new User(Guid.NewGuid(), request.Email, request.FirstName, request.LastName, 
            request.DateOfBirth, request.Nationality, securedPassword, 1);
        
        await _userRepository.AddAsync(user);
    }
}