using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using archNoire.Models;
namespace archNoire.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        [HttpGet]
        public ActionResult Index()
        {
            return View("UserLoginIndex");
        }
        [HttpPost]
        public ActionResult UserLoginIndex(UserAndPage par)
        {
            string logInEmail = par.user.email;
            string logInPassword = par.user.password;
            // validation here
            if(logInPassword.Length < 8)
            {
                ViewBag.message = "Invalid Password! (i.e. lessthan 8 characters)";
                return View("UserLoginIndex");
            }
            System.Diagnostics.Debug.WriteLine("emil : ");
            System.Diagnostics.Debug.WriteLine(logInEmail);
            // to do sql validastion
            return View("UserLoginIndex");
            
        }

        [HttpPost]
        public ActionResult PageLoginIndex(UserAndPage par)
        {
            string logInEmail = par.page.email;
            string logInPassword = par.page.password;
            // validation here
            if (logInPassword.Length < 8)
            {
                ViewBag.message = "Invalid Password! (i.e. lessthan 8 characters)";
                return View("UserLoginIndex");
            }
            System.Diagnostics.Debug.WriteLine("emil : ");
            System.Diagnostics.Debug.WriteLine(logInEmail);
            // to do sql validastion
            return View("UserLoginIndex");
        }
    }
}