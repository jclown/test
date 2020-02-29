using Microsoft.AspNetCore.Mvc.Filters;

namespace Modobay
{
    public interface IAuthenticationProxy
    {
        UserDto SetCurrentUser(string token, ActionExecutingContext checkToken = null);

        UserDto SetCurrentGuest(string guestId);
    }
}