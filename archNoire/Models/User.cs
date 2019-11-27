using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace archNoire.Models
{
    public class User
    {
        public string name { set; get; }
        public string location { set; get; }
        public string password { set; get; }
        public string check_password { set; get; }
        public string email { set; get; }
        public string  gender { set; get; }
        public DateTime birth_date { set; get; }
        public string phone_number { set; get; }
        public string bio { set; get; }

  
      
    }
}