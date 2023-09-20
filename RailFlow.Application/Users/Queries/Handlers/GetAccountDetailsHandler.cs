using MediatR;
using RailFlow.Application.Exceptions;
using RailFlow.Application.Users.DTO;
using Railflow.Core.Repositories;
using Railflow.Core.Services;

namespace RailFlow.Application.Users.Queries.Handlers;

internal sealed class GetAccountDetailsHandler : IRequestHandler<GetAccountDetails, UserDetailsDto>
{
    private readonly IContextService _contextService;
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public GetAccountDetailsHandler(IContextService contextService, IUserRepository userRepository, IUserMapper userMapper)
    {
        _contextService = contextService;
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<UserDetailsDto> Handle(GetAccountDetails request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(_contextService.UserId);
        
        if (user is null)
        {
            throw new UserNotFoundException(_contextService.UserId);
        }
            
        return _userMapper.MapUserDetailsDto(user);
    }
}