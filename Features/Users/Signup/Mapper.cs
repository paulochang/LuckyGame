using LuckyGame.Infrastructure.Entities;

namespace LuckyGame.Features.Users.Signup;

public class Mapper : Mapper<Request, Response, User>
{
    public override User ToEntity(Request r) => new()
    {
        Username = r.Username.ToLower(),
        Passwordhash = BCrypt.Net.BCrypt.HashPassword(r.Password),
        Account = 10000
    };
}