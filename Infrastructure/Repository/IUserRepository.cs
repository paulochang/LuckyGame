using LuckyGame.Infrastructure.Entities;

namespace LuckyGame.Infrastructure.Repository;

public interface IUserRepository
{
    Task<bool> CreateAsync(User user);

    Task<User?> GetAsync(String username);

    Task<IEnumerable<User>> GetAllAsync();

    Task<bool> UpdateAsync(User user);

    Task<bool> DeleteAsync(User username);
}