using RailFlow.Application.Users.DTO;

namespace RailFlow.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(int userId, string role);
}