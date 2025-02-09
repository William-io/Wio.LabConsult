using System.Collections;
using Wio.LabConsult.Application.Persistence;

namespace Wio.LabConsult.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable? _repositories;
    private readonly LabConsultDbContext _context;

    public UnitOfWork(LabConsultDbContext context)
    {
        _context = context;
    }

    public Task<int> Complete()
    {
        try
        {
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error na transação: " + e.Message);
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (_repositories is null)
        {
            _repositories = new Hashtable();
        }

        var type = typeof(TEntity).Name;

        if(!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryBase<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IAsyncRepository<TEntity>)_repositories[type]!;
    }
}
