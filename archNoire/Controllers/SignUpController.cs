using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using archNoire.Models;
using archNoire.DBControllers;
namespace archNoire.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        private UserDBController userController;
        public ActionResult Index()
        {
            userController = new UserDBController();
            ViewBag.message = "";
            return View("UserSignUpIndex");
        }

        [HttpPost]
        public ActionResult UserSignUpIndex(UserAndPage par) {

            string name = par.user.name;
            string password = par.user.password;
            string check_password = par.user.check_password;
            string phone = par.user.phone_number;
            string location = par.user.location;
            string gender = par.user.gender;
            DateTime birth_date = par.user.birth_date;
            string email = par.user.email;
            if (password == check_password)
            {
                if (password.Length < 8)
                {
                    ViewBag.message = "Invalid Password! (i.e. lessthan 8 characters)";
                    return View("UserLoginIndex");
                }
                if(phone[0] != '0' || phone[1] != '1')
                {
                    ViewBag.message = "Invalid Phone Number! (i.e. Must be in the format 01xxxxxxxxx, exactly 11 digit)";
                    return View("UserLoginIndex");
                }
                if (location.Length < 3)
                {
                    ViewBag.message = "Invalid Location! (i.e. Must be more than 3 characters)";
                    return View("UserLoginIndex");
                }
                if (birth_date.Year < 1920 || birth_date.Year > 2020 || birth_date.Month < 1 || birth_date.Day < 1)
                {
                    ViewBag.message = "Invalid BirthDate! (i.e. Must be in the format mm/dd/yyyy)";
                    return View("UserLoginIndex");
                }
                if ( phone != "" && location != "" && gender != "" && name != "" && password != "")
                {
                   
                    return RedirectToAction("InitialSignUp", "User",par.user );
                }
                else
                {
                    ViewBag.message = "please fill all fields";
                    return View("UserSignUpIndex");
                }
            }
            else
            {
                ViewBag.message = "please make sure you entered the same passwords";
                return View("UserSignUpIndex");
            }
        }


        [HttpPost]
        public ActionResult PageSignUpIndex(UserAndPage par) {

            string name = par.page.name;
            string password = par.page.password;
            string phone = par.page.phone_number;
            string email = par.page.email;
            string location = par.page.location;
            if (password != "")
            {
                if( phone != "" && location != ""  && name != "" && email != "")
                {
                    return View("../Home/index");
                }
                else
                {
                    ViewBag.message = "please fill all fields";
                    return View("UserSignUpIndex");
                }
            }
            else
            {
                ViewBag.message = "please make sure you entered the same passwords";
                return View("UserSignUpIndex");
            }
        }

    }
}
