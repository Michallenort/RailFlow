using MediatR;
using RailFlow.Application.Exceptions;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Users.Commands.Handlers;

internal sealed class DeleteUserHandler : IRequestHandler<DeleteUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    
    public DeleteUserHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }
    
    public async Task Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        
        var role = await _roleRepository.GetByIdAsync(user.RoleId);

        if (role!.Name == "Supervisor")
        {
            throw new DeleteException("Supervisor");
        }
        
        await _userRepository.DeleteAsync(user);
    }
}