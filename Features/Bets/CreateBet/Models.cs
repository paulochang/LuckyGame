namespace LuckyGame.Features.Bets.CreateBet;

public class Request
{
    [FromClaim]
    public string UserID { get; set; }
    public int Points { get; init; }
    public int Number { get; init; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Points)
            .NotEmpty().WithMessage("A betting amount is required");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("A number must be chosen")
            .GreaterThanOrEqualTo(0).WithMessage("The number must be between 0 and 9")
            .LessThanOrEqualTo(9).WithMessage("The number must be between 0 and 9");
    }
}

public class Response
{
    public int Account { get; init; }
    public string Status { get; init; }
    public string Points { get; init; }
}
