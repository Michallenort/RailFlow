using MediatR;
using RailFlow.Application.Users.DTO;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Users.Queries.Handlers;

internal sealed class GetUsersHandler : IRequestHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;
    
    public GetUsersHandler(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }
    
    public async Task<IEnumerable<UserDto>> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        return _userMapper.MapUserDtos(users);
    }
}