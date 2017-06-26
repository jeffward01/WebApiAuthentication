using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using CPL.Models;

namespace CPL.Data.Infrastructure
{
    public class CplContext : IdentityDbContext
    {
        public CplContext() : base("CplContext")
        {
            //     this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<NotificationBlast> NotificationBlasts { get; set; }
        public DbSet<PreLaunchNotyUser> PreLaunchNotyUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var notyBlastConfig = modelBuilder.Entity<NotificationBlast>();
            notyBlastConfig.HasKey(_ => _.NotificationBlastId);

            var appUserConfig = modelBuilder.Entity<PreLaunchNotyUser>();
            appUserConfig.HasKey(_ => _.AppUserId);



            base.OnModelCreating(modelBuilder);
        }
    }
}