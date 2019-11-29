using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using archNoire.Models;

namespace archNoire.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLoginIndex(Admin admin)
        {
            string logInEmail = admin.email;
            string logInPassword = admin.password;
            System.Diagnostics.Debug.WriteLine("emil : ");
            System.Diagnostics.Debug.WriteLine(logInEmail);

            // to do sql validastion
            return View("Index");
        }



    }
}