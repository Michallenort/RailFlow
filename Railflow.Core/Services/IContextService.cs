namespace Railflow.Core.Services;

public interface IContextService
{
    Guid? UserId { get; }
    string? Role { get; }
}