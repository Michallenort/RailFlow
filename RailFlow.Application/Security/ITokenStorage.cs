using RailFlow.Application.Users.DTO;

namespace RailFlow.Application.Security;

public interface ITokenStorage
{
    void Set(JwtDto dto);
    JwtDto Get();
}