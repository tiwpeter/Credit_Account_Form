using CreditAccountApi.Entities;

namespace CreditAccountApi.Common.Interfaces;

public interface IRegisterRepository
{
    Task<RegisterFrom?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<RegisterFrom>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RegisterFrom> CreateAsync(RegisterFrom registerFrom, CancellationToken cancellationToken = default);
    Task UpdateAsync(RegisterFrom registerFrom, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}