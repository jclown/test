namespace Modobay.Api
{
    public interface IUserBehavior
    {
        int AddUserBehavior(UserBehaviorAttribute userBehavior, UserDto userDto);
    }
}