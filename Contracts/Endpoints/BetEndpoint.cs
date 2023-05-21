using System.Diagnostics;
using LuckyGame.Contracts.Requests;
using LuckyGame.Contracts.Response;

namespace LuckyGame.Contracts.Endpoints;

public class BetEndpoint : Endpoint<CreateBetRequest, BetReponse>
{
    private int _account = 10000;
    public override void Configure()
    {
        Post("/api/bet/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBetRequest req, CancellationToken ct)
    {
        //this is not thread-safe but we'll use it for now just to POC
        Random rnd = new Random();
        
        var randNumber = rnd.Next(10);
        Debug.Print(randNumber.ToString());
        BetReponse response;
        if (randNumber == req.Number) {
            response = new BetReponse()
            {
                Account = _account + req.Points,
                Points = "+" + req.Points,
                Status = "won"
            };
        } else {
            response = new BetReponse()
            {
                Account = _account - req.Points,
                Points = "-" + req.Points,
                Status = "lost"
            };
        }

        await SendAsync(response);
    }
}