using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class Post
    {
        [Display(Name = "User ID")]
        public int userID { set; get; }

        [Display(Name = "Post ID")]
        public int postID { set; get; }

        [Display(Name = "Post")]
        public string text { get; set; }

        [Display(Name = "Location")]
        public string location { get; set; }

        [Display(Name = "Date")]
        public DateTime date { get; set; }
       


    }
}