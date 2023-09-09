using Microsoft.Extensions.DependencyInjection;

namespace RailFlow.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}