using LuckyGame.Infrastructure.Services;

namespace LuckyGame.Features.Users.Signup;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    private readonly IUserService _userService;

    public Endpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override void Configure()
    {
        Post("/users/signup");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var user = Map.ToEntity(req);
        
        var userNameIsTaken = await _userService.UserNameIsTaken(user.Username.ToLower());

        if (userNameIsTaken)
            AddError(r => r.Username, "sorry! that username is not available...");

        ThrowIfAnyErrors();

        await _userService.CreateNewUser(user);

        await SendAsync(new()
        {
            Message = "Thank you for signing up as a player!"
        });
    }
}