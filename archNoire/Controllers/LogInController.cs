﻿using System;
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
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            string logInEmail = user.email;
            string logInPassword = user.password;
            System.Diagnostics.Debug.WriteLine("emil : ");
            System.Diagnostics.Debug.WriteLine(logInEmail);

            // to do sql validastion
            return View();
            
        }
    }
}