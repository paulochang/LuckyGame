using LuckyGame.Auth;
using LuckyGame.Infrastructure.Services;

namespace LuckyGame.Features.Bets.CreateBet;
public class Endpoint : Endpoint<Request, Response>
{
    private readonly IRandomService _randomService;
    private readonly IUserService _userService;

    public Endpoint(IRandomService randomService, IUserService userService)
    {
        _randomService = randomService;
        _userService = userService;
    }

    public override void Configure()
    {
        Post("/api/bets");
        Claims(Claim.UserID);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var randNumber = _randomService.next();

        var user = await _userService.GetUserByUsername(req.UserID);
        var account = user.Account;
        
        Response response;
        if (randNumber == req.Number) {
            account += req.Points;
            response = new Response
            {
                Account = account,
                Points = "+" + req.Points,
                Status = "won"
            };
        } else {
            account -= req.Points;
            response = new Response
            {
                Account = account,
                Points = "-" + req.Points,
                Status = "lost"
            };
        }

        await _userService.UpdateAccount(user.Username, account);

        await SendAsync(response);
    }
}