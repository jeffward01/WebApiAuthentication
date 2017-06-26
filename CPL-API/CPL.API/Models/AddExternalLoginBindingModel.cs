using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPL.API.Models
{
    public class AddExternalLoginBindingModel
    {

        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}