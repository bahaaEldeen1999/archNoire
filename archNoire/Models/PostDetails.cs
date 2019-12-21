using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class PostDetails
    {
        public userPost post { get; set; }
        public List<Comment> comments { get; set; }

    }

    public class Comment
    {

        public int user_posted_id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Post ID")]
        public int post_id { get; set; }

        [Display(Name = "Comment ID")]
        public int comment_id { get; set; }

        [Display(Name = "User ID")]
        public int user_commented_id { get; set; }

        [Display(Name = "Comment")]
        public string text { get; set; }

        [Display(Name = "likes")]
        public int no_of_likes { get; set; }
    }
}