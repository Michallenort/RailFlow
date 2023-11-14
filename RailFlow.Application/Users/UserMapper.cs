using System.Collections;
using RailFlow.Application.Users.DTO;
using Railflow.Core.Entities;

namespace RailFlow.Application.Users;

internal interface IUserMapper
{
    IEnumerable<UserDto> MapUserDtos(IEnumerable<User> users);
    IEnumerable<UserDetailsDto> MapUserDetailsDtos(IEnumerable<User> users);
    UserDetailsDto MapUserDetailsDto(User user);
}

internal sealed class UserMapper : IUserMapper
{
    public IEnumerable<UserDto> MapUserDtos(IEnumerable<User> users)
        => users.Select(x => new UserDto(x.Id, $"{x.FirstName} {x.LastName}", x.Email));

    public IEnumerable<UserDetailsDto> MapUserDetailsDtos(IEnumerable<User> users)
        => users.Select(x => new UserDetailsDto(x.Id, x.FirstName, x.LastName, x.Email,
            x.DateOfBirth, x.Nationality, x.Role.Name));

    public UserDetailsDto MapUserDetailsDto(User user)
        => new (user.Id, user.FirstName, user.LastName, user.Email,
            user.DateOfBirth, user.Nationality, user.Role.Name);
}