using LuckyGame.Infrastructure.Services;

namespace LuckyGame.Features.Users.Login;

public class Endpoint : Endpoint<Request, Response>
{
    private readonly IUserService _userService;

    public Endpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = await _userService.GetUserByUsername(req.Username);
        
        if (user?.Passwordhash is null)
            ThrowError("No user found with that username!");
        
        if (!BCrypt.Net.BCrypt.Verify(req.Password, user.Passwordhash))
            ThrowError("Password is incorrect!");

        Response.Username = user.Username;
        Response.Points = user.Account;

        DateTime expiryDate = DateTime.UtcNow.AddHours(4);
        Response.Token.ExpiryDate = expiryDate;
        Response.Token.Value = JWTBearer.CreateToken(
            signingKey: Config["JwtSigningKey"],
            expireAt: expiryDate,
            claims: (Claim.UserID, user.Username));

        await SendAsync(Response);
    }
}