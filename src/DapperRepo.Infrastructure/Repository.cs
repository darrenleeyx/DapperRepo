using System.Data;

namespace DapperRepo.Infrastructure;

internal abstract class Repository
{
    protected IDbTransaction Transaction { get; private set; }
    protected IDbConnection? Connection { get { return Transaction.Connection; } }

    public Repository(IDbTransaction transaction)
    {
        Transaction = transaction;
    }
}
