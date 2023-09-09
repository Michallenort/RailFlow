using Microsoft.EntityFrameworkCore;

namespace RailFlow.Infrastructure.DAL;

internal sealed class TrainDbContext : DbContext
{
    public TrainDbContext(DbContextOptions<TrainDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}