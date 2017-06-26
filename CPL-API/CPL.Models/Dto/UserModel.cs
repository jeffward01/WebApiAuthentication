using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CPL.Models.Dto
{
    [DataContract]
    public class UserModel
    {
        //   [Microsoft.Build.Framework.Required]
        [DataMember]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        // [Microsoft.Build.Framework.Required]
        [DataMember]
        [StringLength(100, ErrorMessage = "The {0} nust be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataMember]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataMember(Name = "smsNotyEnabled")]
        public bool SmsNotyEnabled { get; set; }

        [DataMember(Name = "emailNotyEnabled")]
        public bool EmailNotyEnabled { get; set; }

        [DataMember(Name = "emailAddress")]
        public string EmailAddress { get; set; }

        [DataMember(Name = "mobilePhone")]
        public string MobilePhone { get; set; }
    }
}