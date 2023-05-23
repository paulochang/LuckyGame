namespace LuckyGame.Features.Users.Signup;

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
            .NotEmpty().WithMessage("Username is required!")
            .MinimumLength(3).WithMessage("The provided username is too short!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A password is required!")
            .MinimumLength(6).WithMessage("The password is too short!")
            .MaximumLength(250).WithMessage("The password is too long!");
    }
}

public class Response
{
    public string Message { get; set; }
} 