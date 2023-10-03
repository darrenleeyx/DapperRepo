using DapperRepo.Application;
using DapperRepo.Domain;
using System.Data;

namespace DapperRepo.Infrastructure;

internal class UsersRepository : Repository, IUsersRepository
{
    public UsersRepository(IDbTransaction transaction)
        : base(transaction)
    {
    }

    public void Add(User user)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public User GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(User user)
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }
}
