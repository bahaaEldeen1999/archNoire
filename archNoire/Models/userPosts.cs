using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class userPosts
    {
        [Display(Name = "User ID")]
        public int user_id { get; set; }

        [Display(Name = "User Name")]
        public string name { get; set; }

        public List<userPost> posts { get; set; }



    }

    public class userPost
    {
        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "User ID")]
        public int user_id { get; set; }

        [Display(Name = "Post ID")]
        public int post_id { get; set; }

        [Display(Name = "Post")]
        public string text { get; set; }

        [Display(Name = "Date")]
        public DateTime date { get; set; }

        [Display(Name = "Location")]
        public string location { get; set; }

        [Display(Name = "Number of likes")]
        public int no_of_likes { get; set; }

    }

    

}