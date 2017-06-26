using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPL.Models;

namespace CPL.Data
{
    public class PreLaunchNotyUserRepository : IPreLaunchNotyUserRepository
    {
        public PreLaunchNotyUser Create(PreLaunchNotyUser preLaunchUser)
        {
            using (var context = new CPL.Data.Infrastructure.CplContext())
            {
                context.PreLaunchNotyUsers.Add(preLaunchUser);
                context.SaveChanges();
                return preLaunchUser;
            }
        }

        public PreLaunchNotyUser GetByAppUserId(int appUserId)
        {
            using (var context = new CPL.Data.Infrastructure.CplContext())
            {
               
                return context.PreLaunchNotyUsers.Find(appUserId);
            }
        }

        public bool IsUserNameTaken(string username)
        {
            using (var context = new CPL.Data.Infrastructure.CplContext())
            {

                return context.PreLaunchNotyUsers.Any(_ => _.UserName == username);
            }
        }

        public PreLaunchNotyUser GetByAppUserId(string identityUserId)
        {
            using (var context = new CPL.Data.Infrastructure.CplContext())
            {

                return context.PreLaunchNotyUsers.FirstOrDefault(_ => _.AppIdentityUserId == identityUserId);
            }
        }

        public PreLaunchNotyUser Update(PreLaunchNotyUser preLaunchUser)
        {
            using (var context = new CPL.Data.Infrastructure.CplContext())
            {
                context.Entry(preLaunchUser).State = EntityState.Modified;
                context.SaveChanges();
                return preLaunchUser;
            }
        }

        public bool Delete(PreLaunchNotyUser preLaunchUser)
        {
            using (var context = new CPL.Data.Infrastructure.CplContext())
            {
                context.Entry(preLaunchUser).State = EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
        }
    }
}
