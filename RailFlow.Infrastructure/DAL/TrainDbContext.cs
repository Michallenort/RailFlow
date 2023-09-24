using Microsoft.EntityFrameworkCore;
using Railflow.Core.Entities;

namespace RailFlow.Infrastructure.DAL;

internal sealed class TrainDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Station> Stations { get; set; }
    
    public TrainDbContext(DbContextOptions<TrainDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}