using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class PagePosts
    {
        [Display(Name = "Page ID")]
        public int page_id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        public List<pagePost> posts { get; set; }

    }


    public class pagePost
    {
        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Page ID")]
        public int page_id { get; set; }

        [Display(Name = "Post ID")]
        public int page_post_id { get; set; }

        [Display(Name = "Date")]
        public DateTime date { get; set; }

        [Display(Name = "Location")]
        public string location { get; set; }

        [Display(Name = "Likes")]
        public int no_of_likes { get; set; }

        [Display(Name = "Post")]
        public string text { get; set; }

    }
}