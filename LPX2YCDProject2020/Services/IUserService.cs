namespace LPX2YCDProject2020.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}