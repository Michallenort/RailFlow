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
            new Role()
            {
                Name = "User"
            },
            new Role()
            {
                Name = "Employee"  
            },
            new Role()
            {
                Name = "Supervisor"
            }
        };

        return roles;
    }

    private IEnumerable<User> getUsers()
    {
        var users = new List<User>()
        {
            new User()
            {
                Email = "user1@gmail.com",
                FirstName = "User 1",
                LastName = "User",
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1990, 1, 1), DateTimeKind.Utc),
                Nationality = "Poland",
                RoleId = 1
            },
            new User()
            {
                Email = "employee1@gmail.com",
                FirstName = "Employee 1",
                LastName = "User",
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1998, 2, 10), DateTimeKind.Utc),
                Nationality = "Poland",
                RoleId = 2
            },
            new User()
            {
                Email = "supervisor1@gmail.com",
                FirstName = "Supervisor 1",
                LastName = "User",
                DateOfBirth = DateTime.SpecifyKind(new DateTime(1988, 3, 13), DateTimeKind.Utc),
                Nationality = "USA",
                RoleId = 3
            }
        };
        
        users[0].PasswordHash = _passwordManager.Secure("userPassword1");
        users[1].PasswordHash = _passwordManager.Secure("employeePassword1");
        users[2].PasswordHash = _passwordManager.Secure("supervisorPassword1");
        
        return users;
    }
    
}