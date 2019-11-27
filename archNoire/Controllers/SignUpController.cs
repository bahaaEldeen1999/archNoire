using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using archNoire.Models;

namespace archNoire.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            ViewBag.message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user) {

            string name = user.name;
            string password = user.password;
            string check_password = user.check_password;
            string phone = user.phone_number;
            string location = user.location;
            string gender = user.gender;
            System.Diagnostics.Debug.WriteLine(gender );
            DateTime birth_date = user.birth_date;
            if (password == check_password)
            {
                if( phone != "" && location != "" && name != "" && password != "")
                {
                    return View("../Home/index");
                }
                else
                {
                    ViewBag.message = "please fill all fields";
                    return View();
                }
            }
            else
            {
                ViewBag.message = "please make sure you entered the same passwords";
                return View();
            }
        }

    }
}