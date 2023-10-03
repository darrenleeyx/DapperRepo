namespace DapperRepo.Application;

public interface IUnitOfWork : IDisposable
{


    void Commit();
}
