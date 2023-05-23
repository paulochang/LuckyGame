namespace LuckyGame.Features.Users.Login;

public class Request
{
    public string Username { get; init; }
    public string Password { get; init; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A password is required!");
    }
}

public class Response
{
    
    public string Username { get; set; }
    public int Points { get; set; }
    public JwtToken Token { get; set; } = new();
} 