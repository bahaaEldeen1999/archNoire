using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class Admin
    {
        [Display(Name ="Admin ID")]
        public int admin_id { get; set; }

        [Display(Name = "Name")]
        public string name { set; get; }

        [Display(Name = "Email")]
        public string admin_email { set; get; }

        [Display(Name = "Password")]
        public string password { set; get; }

    }
}