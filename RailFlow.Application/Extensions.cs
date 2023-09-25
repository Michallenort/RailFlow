using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MediatR.NotificationPublishers;
using RailFlow.Application.Stations;
using RailFlow.Application.Stations.Commands;
using RailFlow.Application.Stations.Validators;
using RailFlow.Application.Users;
using RailFlow.Application.Users.Commands;
using RailFlow.Application.Users.Validators;

namespace RailFlow.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.Scan(s => s.FromAssemblies(typeof(IRequestHandler<>).Assembly)
        //     .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
        //     .AsImplementedInterfaces()
        //     .WithScopedLifetime());
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddScoped<IValidator<SignUp>, SignUpValidator>();
        services.AddScoped<IValidator<CreateUser>, CreateUserValidator>();
        services.AddScoped<IValidator<UpdateAccount>, UpdateAccountValidator>();
        services.AddScoped<IValidator<CreateStation>, CreateStationValidator>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<IStationMapper, StationMapper>();
        
        return services;
    }
}