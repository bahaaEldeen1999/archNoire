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
        private int adminID;
        // GET: Admin
        public ActionResult Index(int id)
        {
            adminID = id;
            ViewBag.adminID = adminID;
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Admin admin)
        {
            string logInEmail = admin.email;
            string logInPassword = admin.password;
           // System.Diagnostics.Debug.WriteLine("emil : ");
            //System.Diagnostics.Debug.WriteLine(logInEmail);

            // to do sql validastion
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            string SignUpEmail = admin.email;
            string SignUpPassword = admin.password;
            string name = admin.name;
            // System.Diagnostics.Debug.WriteLine("emil : ");
            //System.Diagnostics.Debug.WriteLine(logInEmail);

            // to do sql validastion
            return View("index");
        }



    }
}