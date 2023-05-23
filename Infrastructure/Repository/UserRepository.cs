using Dapper;
using LuckyGame.Infrastructure.Database;
using LuckyGame.Infrastructure.Entities;

namespace LuckyGame.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<bool> CreateAsync(User user)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"INSERT INTO Users (Username, Passwordhash, Account) 
            VALUES (@Username, @Passwordhash, @Account)",
            user);
        return result > 0;
    }

    public async Task<User?> GetAsync(string username)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Username = @Username LIMIT 1", new { Username = username });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<User>("SELECT * FROM Users");
    }

    public async Task<bool> UpdateAsync(User user)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            @"UPDATE Users SET Passwordhash = @Passwordhash, Account = @Account WHERE Username = @Username",
            user);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(User username)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"DELETE FROM Users WHERE Username = @Username",
            new {Username = username});
        return result > 0;
    }
}