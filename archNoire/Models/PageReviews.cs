using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class PageReviews
    {
        [Display(Name = "Page ID")]
        public int page_id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        public List<Review> reviews { get; set; }
    }

    public class Review
    {
        [Display(Name = "User ID")]
        public int user_id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Page ID")]
        public int page_id { get; set; }

        [Display(Name = "Review")]
        public string text { get; set; }

    }
    
}