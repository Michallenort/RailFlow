using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Railflow.Core.Repositories;
using RailFlow.Infrastructure.DAL.Decorators;
using RailFlow.Infrastructure.DAL.Repositories;
using Supabase;

namespace RailFlow.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "postgres";

    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);
        services.AddDbContext<TrainDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IStationRepository, StationRepository>();
        services.AddScoped<ITrainRepository, TrainRepository>();
        services.AddScoped<IRouteRepository, RouteRepository>();
        services.AddScoped<IStopRepository, StopRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<IEmployeeAssignmentRepository, EmployeeAssignmentRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        
        services.AddHostedService<Seeder>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.TryDecorate(typeof(IRequestHandler<>), typeof(UnitOfWorkDecorator<>));
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        return services;
    }

    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var projectUrl = configuration["Storage:ProjectUrl"];
        var apiKey = configuration["Storage:ApiKey"];
        
        services.AddScoped<Client>(_ => 
            new Client(projectUrl, apiKey, new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            }));

        return services;
    }
}