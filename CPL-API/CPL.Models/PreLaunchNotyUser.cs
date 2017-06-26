using System;

namespace CPL.Models
{
    public class PreLaunchNotyUser
    {
        public int AppUserId { get; set; }

        public string AppIdentityUserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public bool EmailNotifications { get; set; }
        public bool SmsNotifications { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}