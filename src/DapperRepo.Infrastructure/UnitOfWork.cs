using DapperRepo.Application;
using System.Data;

namespace DapperRepo.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private IDbConnection _connection;
    private IDbTransaction _transaction;
    private IUsersRepository? _usersRepository;
    private bool _disposed;

    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection;
        _transaction = _connection.BeginTransaction();
    }

    public IUsersRepository UsersRepository
    {
        get { return _usersRepository ?? (_usersRepository = new UsersRepository(_transaction)); }
    }

    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
            resetRepositories();
        }
    }

    public void Dispose()
    {
        dispose(true);
        GC.SuppressFinalize(this);
    }

    private void resetRepositories()
    {
        _usersRepository = null;
    }

    private void dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                }
            }
            _disposed = true;
        }
    }

    ~UnitOfWork()
    {
        dispose(false);
    }
}
