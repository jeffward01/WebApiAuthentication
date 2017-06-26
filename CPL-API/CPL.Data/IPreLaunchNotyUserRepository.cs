using CPL.Models;

namespace CPL.Data
{
    public interface IPreLaunchNotyUserRepository
    {
        PreLaunchNotyUser Create(PreLaunchNotyUser preLaunchUser);
        PreLaunchNotyUser GetByAppUserId(int appUserId);
        PreLaunchNotyUser GetByAppUserId(string identityUserId);
        PreLaunchNotyUser Update(PreLaunchNotyUser preLaunchUser);
        bool Delete(PreLaunchNotyUser preLaunchUser);
        bool IsUserNameTaken(string username);
    }
}