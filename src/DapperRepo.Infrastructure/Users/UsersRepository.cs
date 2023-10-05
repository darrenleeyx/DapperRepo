using DapperRepo.Application.Common.Abstractions;
using DapperRepo.Domain;
using DapperRepo.Infrastructure.Common;
using SqlKata.Compilers;
using System.Data;

namespace DapperRepo.Infrastructure.Users;

internal class UsersRepository : Repository<User>, IUsersRepository
{
    public UsersRepository(IDbTransaction transaction, Compiler compiler, int commandTimeout)
        : base(transaction, compiler, commandTimeout)
    {
    }
}
