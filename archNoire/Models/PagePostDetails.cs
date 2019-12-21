using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class PagePostDetails
    {
        public pagePost post { get; set; }
        public List<Comment> comments { get; set; }
    }
}