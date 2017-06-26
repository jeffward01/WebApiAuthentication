using System.Threading.Tasks;
using CPL.Models.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CPL.Core.Managers
{
    public interface IAuthManager
    {
        IdentityResult RegisterUser(UserModel registerModel);
        Task<IdentityUser> FindUser(string username, string password);
        void Dispose();
    }
}