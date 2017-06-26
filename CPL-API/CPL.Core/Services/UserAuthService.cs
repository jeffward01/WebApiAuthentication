using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPL.Data;

namespace CPL.Core.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IPreLaunchNotyUserRepository _preLaunchNotyUserRepository;

        public UserAuthService(IPreLaunchNotyUserRepository preLaunchNotyUserRepository)
        {
            _preLaunchNotyUserRepository = preLaunchNotyUserRepository;
        }

        public bool IsUsernameTaken(string userName)
        {
            return _preLaunchNotyUserRepository.IsUserNameTaken(userName);
        }
    }
}
