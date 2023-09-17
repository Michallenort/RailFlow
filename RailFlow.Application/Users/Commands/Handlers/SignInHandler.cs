using MediatR;
using RailFlow.Application.Exceptions;
using RailFlow.Application.Security;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Users.Commands.Handlers;

internal sealed class SignInHandler : IRequestHandler<SignIn>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;
    
    public SignInHandler(IUserRepository userRepository, IRoleRepository roleRepository, IAuthenticator authenticator, 
        IPasswordManager passwordManager, ITokenStorage tokenStorage)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }
    
    public async Task Handle(SignIn request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new InvalidEmailOrPasswordException();
        }
        
        if (!_passwordManager.Validate(request.Password, user.PasswordHash))
        {
            throw new InvalidEmailOrPasswordException();
        }
        
        var role = await _roleRepository.GetByIdAsync(user.RoleId);
        
        var jwt = _authenticator.CreateToken(user.Id, role.Name);
        _tokenStorage.Set(jwt);
    }
}