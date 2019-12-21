using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class PageEvents
    {
        public int page_id { get; set; }
        public string name { get; set; }

        public List<EventData> events { get; set; }
    }

    public class EventData
    {
        public int page_id { get; set; }
        public int event_id { get; set; }
        public DateTime date { get; set; }
        public string eventLocation { get; set; }
        public int ticket_price { get; set; }
        public int no_of_people_going { get; set; }
    }
}