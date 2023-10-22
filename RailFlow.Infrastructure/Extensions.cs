using Hangfire;
using Hangfire.InMemory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Railflow.Core.Entities;
using Railflow.Core.Services;
using RailFlow.Infrastructure.Auth;
using RailFlow.Infrastructure.Services;
using RailFlow.Infrastructure.DAL;
using RailFlow.Infrastructure.Exceptions;
using RailFlow.Infrastructure.Security;

namespace RailFlow.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        services.AddControllers();
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();

        services.AddPostgres(configuration);
        services.AddSecurity();

        JobStorage.Current = new InMemoryStorage();
        services.AddHangfire(x => x.UseInMemoryStorage());
        
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "RailFlow API",
                Version = "v1"
            });
        });

        services.AddAuth(configuration);
        services.AddScoped<IContextService, ContextService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IConnectionService, ConnectionService>();
        
        return services;
    }
    
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseReDoc(reDoc =>
        {
            reDoc.RoutePrefix = "docs";
            reDoc.SpecUrl("/swagger/v1/swagger.json");
            reDoc.DocumentTitle = "API";
        });
        
        app.UseHangfireServer();
        app.UseHangfireDashboard("/hangfire");
        
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);
        
        return options;
    }
}