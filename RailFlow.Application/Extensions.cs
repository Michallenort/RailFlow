using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MediatR.NotificationPublishers;
using RailFlow.Application.Assignments;
using RailFlow.Application.Assignments.Commands;
using RailFlow.Application.Assignments.Validators;
using RailFlow.Application.Connections;
using RailFlow.Application.Reservations;
using RailFlow.Application.Reservations.Commands;
using RailFlow.Application.Reservations.Validators;
using RailFlow.Application.Routes;
using RailFlow.Application.Routes.Commands;
using RailFlow.Application.Routes.Validators;
using RailFlow.Application.Schedules;
using RailFlow.Application.Stations;
using RailFlow.Application.Stations.Commands;
using RailFlow.Application.Stations.Validators;
using RailFlow.Application.Stops;
using RailFlow.Application.Stops.Commands;
using RailFlow.Application.Stops.Validators;
using RailFlow.Application.Trains;
using RailFlow.Application.Trains.Commands;
using RailFlow.Application.Trains.Validators;
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
        services.AddScoped<IValidator<CreateTrain>, CreateTrainValidator>();
        services.AddScoped<IValidator<CreateRoute>, CreateRouteValidator>();
        services.AddScoped<IValidator<CreateStop>, CreateStopValidator>();
        services.AddScoped<IValidator<UpdateRoute>, UpdateRouteValidator>();
        services.AddScoped<IValidator<CreateAssignment>, CreateAssignmentValidator>();
        services.AddScoped<IValidator<AddReservation>, AddReservationValidator>();
        
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<IStationMapper, StationMapper>();
        services.AddScoped<ITrainMapper, TrainMapper>();
        services.AddScoped<IStopMapper, StopMapper>();
        services.AddScoped<IRouteMapper, RouteMapper>();
        services.AddScoped<IConnectionMapper, ConnectionMapper>();
        services.AddScoped<IAssignmentMapper, AssignmentMapper>();
        services.AddScoped<IScheduleMapper, ScheduleMapper>();
        services.AddScoped<IReservationMapper, ReservationMapper>();
        
        return services;
    }
}