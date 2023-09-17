namespace RailFlow.Infrastructure.DAL;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly TrainDbContext _dbContext;
    
    public UnitOfWork(TrainDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task ExecuteAsync(Func<Task> action)
    { 
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await action();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}