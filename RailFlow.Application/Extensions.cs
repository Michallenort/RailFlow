using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MediatR.NotificationPublishers;
using RailFlow.Application.Users.Commands;
using RailFlow.Application.Users.Validators;

namespace RailFlow.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<SignUp>, SignUpValidator>();
        
        services.Scan(s => s.FromAssemblies(typeof(IRequestHandler<>).Assembly)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                // cfg.NotificationPublisher = new TaskWhenAllPublisher();
                // cfg.NotificationPublisherType = typeof(TaskWhenAllPublisher);
                // cfg.Lifetime = ServiceLifetime.Transient; 
            });
        
        return services;
    }
}