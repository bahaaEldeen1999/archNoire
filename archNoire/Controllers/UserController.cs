﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using archNoire.Models;
namespace archNoire.Controllers
{
    public class UserController : Controller
    {
        private int maxBioLength = 100;
        private string userProfilePhoto = "../Images/userProfilePhoto/defaultIM.jpg";
        // GET: User
        public ActionResult Index()
        {
            ViewBag.Message = "";

            ViewBag.imageSource = userProfilePhoto;
            return View("initialSignUp");
        }
       [HttpPost]
       private ActionResult InitialSignUp(/*User user*/)
        {
            ViewBag.imageSource = userProfilePhoto;
            // ViewBag.user = user;
            return View();
        }

        [HttpPost]
        public ActionResult  UploadInitialImage(HttpPostedFileBase file)
        {
            ViewBag.Message = "";
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("../Images/userProfilePhoto"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    //ViewBag.Message = "image updated successfully";
                    ViewBag.imageSource = "../Images/userProfilePhoto/"+ file.FileName;
                    userProfilePhoto = "../Images/userProfilePhoto/" + file.FileName; 
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "please choose a valid file";
                }
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View("initialSignUp");
        }


        [HttpPost]
        public ActionResult updateBio(User user)
        {
            string userBio = user.bio;
            string imagePath = ViewBag.imageSource;

            if(userBio.Length > maxBioLength)
            {
                ViewBag.Message = "please enter a bio less than 100 characters";
                ViewBag.imageSource = userProfilePhoto;
                return View("initialSignUp");
            }

            // send to dp
            return Content(userBio);
        }

        public ActionResult userSetting()
        {
            return View();
        }
        [HttpPost]
        public ActionResult userSetting(User user)
        {
            // validation

            return View();
        }

        [HttpPost]
        public ActionResult UpdateImage(HttpPostedFileBase file)
        {
            ViewBag.Message = "";
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("../Images/userProfilePhoto"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    //ViewBag.Message = "image updated successfully";
                    ViewBag.imageSource = "../Images/userProfilePhoto/" + file.FileName;
                    userProfilePhoto = "../Images/userProfilePhoto/" + file.FileName;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "please choose a valid file";
                }
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View("userSetting");
        }
    }
}