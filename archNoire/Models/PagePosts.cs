using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class PagePosts
    {
        public int page_id { get; set; }
        public string name { get; set; }

        public List<pagePost> posts { get; set; }

    }


    public class pagePost
    {
        public string name { get; set; }
        public int page_id { get; set; }
        public int page_post_id { get; set; }
        public DateTime date { get; set; }
        public string location { get; set; }
        public int no_of_likes { get; set; }
        public string text { get; set; }

    }
}