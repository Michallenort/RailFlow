using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RailFlow.Application.Security;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>();

        return services;
    }
}