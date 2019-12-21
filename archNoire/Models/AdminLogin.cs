using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class AdminLogin
    {
        [Display(Name = "Admin ID")]
        public int admin_id { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }
    }
}