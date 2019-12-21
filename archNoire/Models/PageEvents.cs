using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class PageEvents
    {
        [Display(Name = "Page ID")]
        public int page_id { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }

        public List<EventData> events { get; set; }
    }

    public class EventData
    {
        [Display(Name = "Page ID")]
        public int page_id { get; set; }

        [Display(Name = "Event ID")]
        public int event_id { get; set; }

        [Display(Name = "Date")]
        public DateTime date { get; set; }

        [Display(Name = "Location")]
        public string eventLocation { get; set; }

        [Display(Name = "Ticket Price")]
        public int ticket_price { get; set; }

        [Display(Name = "People Going")]
        public int no_of_people_going { get; set; }
    }
}