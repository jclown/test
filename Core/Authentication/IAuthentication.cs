namespace Modobay
{
    public interface IAuthentication
    {
        UserDto CheckToken(string token);
    }
}