using MediatR;
using RailFlow.Application.Exceptions;
using RailFlow.Application.Users.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Users.Queries.Handlers;

internal sealed class GetUserDetailsHandler : IRequestHandler<GetUserDetails, UserDetailsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;
    
    public GetUserDetailsHandler(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }
    
    public async Task<UserDetailsDto> Handle(GetUserDetails request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
            
        return _userMapper.MapUserDetailsDto(user);
    }
}