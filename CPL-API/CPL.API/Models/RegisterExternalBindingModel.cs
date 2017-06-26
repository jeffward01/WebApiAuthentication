﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPL.API.Models
{
    public class RegisterExternalBindingModel
    {

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}