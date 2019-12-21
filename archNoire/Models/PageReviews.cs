using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class PageReviews
    {
        public int page_id { get; set; }
        public string name { get; set; }

        public List<Review> reviews { get; set; }
    }

    public class Review
    {
        public int user_id { get; set; }
        public string name { get; set; }
        public int page_id { get; set; }

        [Display(Name = "Review")]
        public string text { get; set; }

    }
    
}