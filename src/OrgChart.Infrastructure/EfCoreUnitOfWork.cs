using Microsoft.EntityFrameworkCore.Storage;
using OrgChart.Core.Interfaces;

namespace OrgChart.Infrastructure;

public class EfCoreUnitOfWork : IUnitOfWork
{
    private readonly OrgChartDbContext _context;
    private IDbContextTransaction? _transaction;

    public EfCoreUnitOfWork(OrgChartDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransaction()
    {
        if (_transaction is not null)
            return;

        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction not started.");

        await _context.SaveChangesAsync();
        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task Rollback()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
    }
}
