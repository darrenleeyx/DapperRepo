namespace DapperRepo.Application.Common.Abstractions;

public interface IRepository<T>
{
    Task<int> AddAsync(T data, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(object id, CancellationToken cancellationToken = default);
    bool Exists();
    Task<IEnumerable<T>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>?> GetByConditionAsync(List<IQueryCondition> conditions, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(object id, T data, CancellationToken cancellationToken = default);
}