using Dapper;

namespace LuckyGame.Infrastructure.Database;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Users (
        Username TEXT NOT NULL,    
        Passwordhash TEXT NOT NULL,
        Account INTEGER)");
    }
}