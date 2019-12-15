using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class Post
    {
        public int userID { set; get; }
        public int postID { set; get; }
        public string text { get; set; }
        public string location { get; set; }
        public DateTime date { get; set; }
       


    }
}