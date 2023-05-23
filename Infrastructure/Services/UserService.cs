using FluentValidation;
using FluentValidation.Results;
using LuckyGame.Infrastructure.Entities;
using LuckyGame.Infrastructure.Repository;

namespace LuckyGame.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> UserNameIsTaken(string username)
    {
        var existingUser = await _userRepository.GetAsync(username);
        return (existingUser is not null);
    }
    
    public async Task<bool> UpdateAccount(string username, int points)
    {
        var existingUser = await _userRepository.GetAsync(username);
        existingUser.Account = points;
        return await _userRepository.UpdateAsync(existingUser);
    }


    public async Task<User?> GetUserByUsername(string username)
    {
        return await _userRepository.GetAsync(username);
    }

    public async Task<bool> CreateNewUser(User user)
    {
        var existingUser = await _userRepository.GetAsync(user.Username);
        if (existingUser is null) return await _userRepository.CreateAsync(user);
        var message = $"A user with username {user.Username} already exists";
        throw new ValidationException(message, new []
        {
            new ValidationFailure(nameof(User), message)
        });

    }
}