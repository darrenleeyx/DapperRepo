using System.Data;

namespace DapperRepo.Infrastructure;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}