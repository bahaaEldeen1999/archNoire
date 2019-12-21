using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class page_admin
    {
        [Display(Name = "ID")]
        public int page_id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Info")]
        public string info { get; set; }

        [Display(Name = "Address")]
        public string location { get; set; }

        [Display(Name = "Email")]
        public string business_email { get; set; }

        [Display(Name = "Phone Number")]
        public string phone_no { get; set; }
    }
}