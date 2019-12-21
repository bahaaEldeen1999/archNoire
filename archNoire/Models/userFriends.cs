using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class userFriends
    {
        [Display(Name = "User ID")]
        public int user_id { get; set; }

        [Display(Name = "User Name")]
        public string name { get; set; }

        public List<user_admin> friends { get; set; }

    }
}