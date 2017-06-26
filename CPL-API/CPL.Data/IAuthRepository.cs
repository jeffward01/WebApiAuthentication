using System.Threading.Tasks;
using CPL.Models;
using CPL.Models.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CPL.Data
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUser(UserModel userModel);
        IdentityResult RegisterUserSync(UserModel userModel, PreLaunchNotyUser appUser);
        Task<IdentityUser> FindUser(string userName, string password);
        IdentityUser FindUserSync(string userName);
        void Dispose();
    }
}