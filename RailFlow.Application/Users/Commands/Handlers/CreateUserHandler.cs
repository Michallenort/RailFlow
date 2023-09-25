using FluentValidation;
using MediatR;
using RailFlow.Application.Exceptions;
using RailFlow.Application.Security;
using RailFlow.Application.Users.Validators;
using Railflow.Core.Entities;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Users.Commands.Handlers;

internal sealed class CreateUserHandler : IRequestHandler<CreateUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IValidator<CreateUser> _validator;
        
    public CreateUserHandler(IUserRepository userRepository, IPasswordManager passwordManager, 
        IRoleRepository roleRepository, IValidator<CreateUser> validator)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _roleRepository = roleRepository;
        _validator = validator;
    }
    
    public async Task Handle(CreateUser request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        if (await _userRepository.GetByEmailAsync(request.Email) is not null)
        {
            throw new EmailExistsException(request.Email);
        }
        
        var securedPassword = _passwordManager.Secure(request.Password);

        var user = new User(Guid.NewGuid(), request.Email, request.FirstName, request.LastName, 
            request.DateOfBirth, request.Nationality, securedPassword, request.RoleId);
        
        await _userRepository.AddAsync(user);
    }
}