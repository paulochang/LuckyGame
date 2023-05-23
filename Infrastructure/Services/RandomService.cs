namespace LuckyGame.Infrastructure.Services;

public class RandomService : IRandomService
{
    private readonly Random _rnd = new Random();
    private const int MaxValue = 10;
    
    public int next()
    {
        return _rnd.Next(MaxValue);
    }
}