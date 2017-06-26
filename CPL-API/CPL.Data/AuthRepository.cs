using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPL.Data.Infrastructure;
using CPL.Models;
using CPL.Models.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CPL.Data
{
    public class AuthRepository :  IDisposable, IAuthRepository
    {
        private readonly CplContext _nootBaseContext;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _nootBaseContext = new CplContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_nootBaseContext));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            //TODO: Add Roles or Claims: http://stackoverflow.com/questions/19541101/add-role-in-asp-net-identity
            //http://stackoverflow.com/questions/28335353/how-to-extend-available-properties-of-user-identity
            var user = new IdentityUser();
            user.UserName = userModel.UserName;

            var result = await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        public IdentityResult RegisterUserSync(UserModel userModel, PreLaunchNotyUser appUser)
        {
            //TODO: Add Roles or Claims: http://stackoverflow.com/questions/19541101/add-role-in-asp-net-identity
            //http://stackoverflow.com/questions/28335353/how-to-extend-available-properties-of-user-identity
            var user = new ApplicationUser();
            user.UserName = userModel.UserName;
            user.PreLaunchUserId = appUser.AppUserId;
            user.EmailNotyEnabled = appUser.EmailNotifications;
            user.MobileNotyEnabled = appUser.SmsNotifications;
            user.PhoneNumber = appUser.MobilePhone;
            user.Email = appUser.EmailAddress;
            var result = _userManager.Create(user, userModel.Password);
            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public IdentityUser FindUserSync(string userName)
        {
            return _userManager.FindByName(userName);
        }




        public void Dispose()
        {
            _nootBaseContext.Dispose();
            _userManager.Dispose();
        }
    }
}
