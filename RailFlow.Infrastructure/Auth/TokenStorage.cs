using Microsoft.AspNetCore.Http;
using RailFlow.Application.Security;
using RailFlow.Application.Users.DTO;

namespace RailFlow.Infrastructure.Auth;

internal sealed class TokenStorage : ITokenStorage
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public TokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public void Set(JwtDto dto) => 
        _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, dto);

    public JwtDto Get()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt))
        {
            return jwt as JwtDto;
        }

        return null;
    }
}