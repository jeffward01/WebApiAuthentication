using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPL.Data;
using CPL.Models;
using CPL.Models.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CPL.Core.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPreLaunchNotyUserRepository _preLaunchUserRepository;

        public AuthManager(IAuthRepository authRepository, IPreLaunchNotyUserRepository preLaunchUserRepository)
        {
            _preLaunchUserRepository = preLaunchUserRepository;
            _authRepository = authRepository;
        }

        public IdentityResult RegisterUser(UserModel registerModel)
        {
            var result = _authRepository.RegisterUserSync(registerModel, new PreLaunchNotyUser());

            var newUser = _authRepository.FindUserSync(registerModel.UserName);
            _createUser(registerModel, new PreLaunchNotyUser(), newUser.Id);


            return IdentityResult.Success;
        }

        private void _createUser(UserModel user, PreLaunchNotyUser preLaunchAppUser, string appUserIdentityId)
        {


            preLaunchAppUser.AppIdentityUserId = appUserIdentityId;
            preLaunchAppUser.UserName = user.UserName;
            preLaunchAppUser.EmailAddress = user.EmailAddress;
            preLaunchAppUser.MobilePhone = user.MobilePhone;
            preLaunchAppUser.EmailNotifications = user.EmailNotyEnabled;
            preLaunchAppUser.SmsNotifications = user.SmsNotyEnabled;
            preLaunchAppUser.CreatedDate = DateTime.UtcNow;

            _preLaunchUserRepository.Create(preLaunchAppUser);
        }

        public async Task<IdentityUser> FindUser(string username, string password)
        {
            return await _authRepository.FindUser(username, password);
        }

        public void Dispose()
        {
            _authRepository.Dispose();
        }
    }
}
