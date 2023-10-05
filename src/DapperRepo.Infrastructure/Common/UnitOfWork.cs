using DapperRepo.Application.Common.Abstractions;
using DapperRepo.Infrastructure.Users;
using SqlKata.Compilers;
using System.Data;

namespace DapperRepo.Infrastructure.Common;

public class UnitOfWork : IUnitOfWork
{
    private IDbConnection _connection;
    private IDbTransaction _transaction;
    private IUsersRepository? _usersRepository;
    private bool _disposed;
    private readonly Compiler _compiler;
    private readonly int _commandTimeout;

    public UnitOfWork(IDbConnection connection)
    {
        _connection = connection;
        _transaction = _connection.BeginTransaction();


        _compiler = new OracleCompiler();
        _commandTimeout = 30;
    }

    public IUsersRepository UsersRepository
    {
        get { return _usersRepository ?? (_usersRepository = new UsersRepository(_transaction, _compiler, _commandTimeout)); }
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
