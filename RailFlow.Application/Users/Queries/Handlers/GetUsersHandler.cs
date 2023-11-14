using System.Collections;
using MediatR;
using RailFlow.Application.Users.DTO;
using Railflow.Core.Entities;
using Railflow.Core.Pagination;
using Railflow.Core.Repositories;

namespace RailFlow.Application.Users.Queries.Handlers;

internal sealed class GetUsersHandler : IRequestHandler<GetUsers, PagedList<UserDetailsDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;
    
    public GetUsersHandler(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }
    
    public async Task<PagedList<UserDetailsDto>> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        IEnumerable<User> users;
        
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            users = await _userRepository.GetBySearchTermAsync(request.SearchTerm);
        }
        else
        {
            users = await _userRepository.GetAllAsync();
        }
        
        var pagedUsers = PagedList<UserDetailsDto>
            .Create(_userMapper.MapUserDetailsDtos(users).ToList(), request.Page, request.PageSize);
        return pagedUsers;
    }
}