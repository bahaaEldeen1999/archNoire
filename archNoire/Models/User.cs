using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace archNoire.Models
{
    public class User
    {
       [Required(ErrorMessage= "Please enter  name more than 3 chars"),MinLength(3)]
        public string name { set; get; }

        [Required(ErrorMessage = "Please enter location more than 3 chars"), MinLength(3)]
        public string location { set; get; }
        [Required(ErrorMessage = "Please enter password more than 7 "), MinLength(3)]
        public string password { set; get; }
        [Required(ErrorMessage = "Please enter same as password"), MinLength(7)]
        public string check_password { set; get; }
        [Required(ErrorMessage = "Please enter valid email")]
        public string email { set; get; }
        [Required(ErrorMessage = "Please enter gender")]
        public string  gender { set; get; }
        [Required(ErrorMessage = "Please enter date")]
        public DateTime birth_date { set; get; }
        [Required(ErrorMessage = "Please enter valid phone"), MinLength(11)]
        public string phone_number { set; get; }
        [Required(ErrorMessage = "Please enter name"), MaxLength(100)]
        public string bio { set; get; }
      
        public string searchedUser { set; get; }
        public string imageSource { set; get; }
        public int userId { set; get; }

  
      
    }
}