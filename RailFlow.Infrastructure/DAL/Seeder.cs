using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RailFlow.Application.Security;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.DAL;

internal sealed class Seeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPasswordManager _passwordManager;

    public Seeder(IServiceProvider serviceProvider, IPasswordManager passwordManager)
    {
        _serviceProvider = serviceProvider;
        _passwordManager = passwordManager;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TrainDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!await dbContext.Roles.AnyAsync())
            {
                var roles = GetRoles();
                await dbContext.Roles.AddRangeAsync(roles, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        
            if (!await dbContext.Users.AnyAsync())
            {
                var users = getUsers();
                await dbContext.Users.AddRangeAsync(users, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) 
        => Task.CompletedTask;

    private IEnumerable<Role> GetRoles()
    {
        var roles = new List<Role>()
        {
            new Role(1, "User"),
            new Role(2, "Employee"),
            new Role(3, "Supervisor")
        };

        return roles;
    }

    private IEnumerable<User> getUsers()
    {
        var users = new List<User>()
        {
            new User(Guid.NewGuid(), "user1@gmail.com", "User 1", "User", 
                new DateTime(1990, 1, 1), "Poland", 
                _passwordManager.Secure("userPassword1"), 1),
            new User(Guid.NewGuid(), "employee1@gmail.com", "Employee 1", "Employee", 
                new DateTime(1986, 10, 1), "Germany", 
                _passwordManager.Secure("employeePassword1"), 2),
            new User(Guid.NewGuid(), "supervisor1@gmail.com", "Supervisor 1", "Supervisor", 
                new DateTime(1970, 5, 12), "USA", 
                _passwordManager.Secure("supervisorPassword1"), 3)
        };
        
        return users;
    }
    
}