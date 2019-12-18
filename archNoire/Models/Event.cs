using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class Event
    {
        public DateTime date { get; set; }
        public int price { get; set; }
        public int noOfPeople { get; set; }
        public string imageSource { get; set; }
        public string location { get; set; }
    }
}