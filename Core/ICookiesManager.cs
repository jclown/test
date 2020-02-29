namespace Modobay
{
    public interface ICookiesManager
    {
        void DeleteCookies(string key);
        string GetCookies(string key);
        void SetCookies(string key, string value, int minutes = 0);
    }
}