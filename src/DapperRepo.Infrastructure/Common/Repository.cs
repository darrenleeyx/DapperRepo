using DapperRepo.Application;
using DapperRepo.Application.Common.Abstractions;
using DapperRepo.Domain.Common.Helpers;
using DapperRepo.Infrastructure.Common.Constants;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;

namespace DapperRepo.Infrastructure.Common;

public abstract class Repository<T> : IRepository<T>
{
    private readonly int _commandTimeout;
    private readonly Compiler _compiler;
    private readonly string _tableName;
    private readonly string _idColumnName;

    protected IDbTransaction Transaction { get; private set; }
    protected IDbConnection? Connection { get { return Transaction.Connection; } }

    protected Repository(IDbTransaction transaction, Compiler compiler, int commandTimeout)
    {
        Transaction = transaction;
        _compiler = compiler;
        _commandTimeout = commandTimeout;

        var type = typeof(T);

        _tableName = AttributesHelper.GetTableName(type) ?? throw new NullReferenceException(nameof(_tableName));
        _idColumnName = AttributesHelper.GetKeyName(type) ?? throw new NullReferenceException(nameof(_idColumnName));
    }

    public bool Exists()
    {
        using (var db = CreateQueryFactory())
        {
            var row = db.Query("DBA_TABLES")
                .Where("TABLE_NAME", _tableName)
                .AsCount()
                .FirstOrDefault();

            if (row == null)
            {
                return false;
            }

            var dictionary = row as IDictionary<string, object>;
            var result = Convert.ToInt32((decimal)dictionary!["count"]);
            return result > 0;
        }
    }

    public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        using (var db = CreateQueryFactory())
        {
            return await db.Query(_tableName)
                .Where(_idColumnName, Comparator.Equal, id)
                .FirstOrDefaultAsync<T?>(transaction: Transaction, cancellationToken: cancellationToken);
        }
    }

    public async Task<IEnumerable<T>?> GetByConditionAsync(List<IQueryCondition> conditions, CancellationToken cancellationToken = default)
    {
        using (var db = CreateQueryFactory())
        {
            var query = db.Query(_tableName);
            query.Where(q =>
            {
                conditions.ForEach(c => { q.Where(c.Field, c.Operator, c.Value); });
                return q;
            });
            _compiler.Compile(query);
            return await query.GetAsync<T>(Transaction, cancellationToken: cancellationToken);
        }
    }

    public async Task<IEnumerable<T>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using (var db = CreateQueryFactory())
        {
            return await db.Query(_tableName)
                .GetAsync<T>(transaction: Transaction, cancellationToken: cancellationToken);
        }
    }

    public async Task<int> AddAsync(T data, CancellationToken cancellationToken = default)
    {
        using (var db = CreateQueryFactory())
        {
            return await db.Query(_tableName)
                .InsertAsync(data, Transaction, cancellationToken: cancellationToken);
        }
    }

    public async Task<int> UpdateAsync(object id, T data, CancellationToken cancellationToken = default)
    {
        using (var db = CreateQueryFactory())
        {
            return await db.Query(_tableName)
                .Where(_idColumnName, Comparator.Equal, id)
                .UpdateAsync(data, Transaction, cancellationToken: cancellationToken);
        }
    }

    public async Task<int> DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        using (var db = CreateQueryFactory())
        {
            return await db.Query(_tableName)
                .Where(_idColumnName, Comparator.Equal, id)
                .DeleteAsync(Transaction, cancellationToken: cancellationToken);
        }
    }

    private QueryFactory CreateQueryFactory()
    {
        return new QueryFactory(Connection, _compiler, _commandTimeout);
    }
}
