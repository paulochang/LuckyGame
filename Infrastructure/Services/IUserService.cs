using LuckyGame.Infrastructure.Entities;

namespace LuckyGame.Infrastructure.Services;

public interface IUserService
{
    public Task<bool> UserNameIsTaken(string username);

    public Task<bool> UpdateAccount(string username, int points);

    public Task<User?> GetUserByUsername(string username);

    public Task<bool> CreateNewUser(User user);
    //var userNameIsTaken = await _userService.UserNameIsTaken(user.Username.ToLower());
}