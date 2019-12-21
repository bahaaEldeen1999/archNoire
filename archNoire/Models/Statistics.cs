using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class Statistics
    {
        [Display(Name = "Users")]
        public int usersCount { get; set; }

        [Display(Name = "Pages")]
        public int pagesCount { get; set; }

        [Display(Name = "Users posts")]
        public int userPostsCount { get; set; }

        [Display(Name = "Pages posts")]
        public int pagePostsCount { get; set; }

        [Display(Name = "Most Active User")]
        public string topUser { get; set; }

        [Display(Name = "Most Active Page")]
        public string pageUser { get; set; }

        [Display(Name = "Events")]
        public int eventsCount { get; set; }




    }
}