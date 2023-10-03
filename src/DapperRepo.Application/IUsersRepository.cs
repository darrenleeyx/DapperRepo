using DapperRepo.Domain;

namespace DapperRepo.Application;

public interface IUsersRepository
{
    void Add(User user);
    void Update(User user);
    User GetById(int id);
    IEnumerable<User> GetAll();
    void Remove(int id);
    void Remove(User user);
}
