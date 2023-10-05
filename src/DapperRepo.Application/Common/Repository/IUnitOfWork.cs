namespace DapperRepo.Application.Common.Abstractions;

public interface IUnitOfWork : IDisposable
{


    void Commit();
}
