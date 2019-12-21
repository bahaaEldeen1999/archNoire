using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class user_admin
    {
        [Display(Name = "ID")]
        public int user_id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Bio")]
        public string bio { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Display(Name = "Phone number")]
        public string phone_no { get; set; }

        [Display(Name = "Address")]
        public string location { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime birth_date { get; set; }

        [Display(Name = "Email")]
        public string personal_email { get; set; }

    }
}