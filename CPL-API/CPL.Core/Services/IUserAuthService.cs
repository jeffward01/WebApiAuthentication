namespace CPL.Core.Services
{
    public interface IUserAuthService
    {
        bool IsUsernameTaken(string userName);
    }
}