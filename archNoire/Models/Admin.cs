﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace archNoire.Models
{
    public class Admin
    {
        public int admin_id { get; set; }
        public string name { set; get; }
        public string admin_email { set; get; }
        public string password { set; get; }

    }
}