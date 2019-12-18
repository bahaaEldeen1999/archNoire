using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using archNoire.DBControllers;
using archNoire.Models;
using System.Data;
namespace archNoire.Controllers
{
    public class PageController : Controller
    {
        private int maxInfoLength = 100;
        static private string pageProfilePhoto = "../../Images/pageProfilePhoto/defaultIM.jpg";
        static private int pageId;
        static private bool isLogged = false;
        static private string pageEmail;
        static private string pagePassword;
        static private string name;
        static private string location;
        static private string pagePhone;
     //   static private DateTime date;
        static private string phone_number;
        static private string info;
        static private string pagePhoto;

        static UserDBController userController = new UserDBController();
        static PageDBController pageController = new PageDBController();
        [HttpPost]
        public ActionResult Index(Page page)
        {
            // get Page id
            pageEmail = page.email;
            pagePassword = page.password;
            DataTable dt = pageController.getPageInfo(pageEmail, pagePassword);
            if (dt != null && dt.Rows.Count != 0)
            {
                isLogged = true;
                pageId = Convert.ToInt32(dt.Rows[0][0].ToString());

                DataTable dPhoto = pageController.getPagePhoto(pageId);


                name = dt.Rows[0]["name"].ToString();
                location = dt.Rows[0]["location"].ToString();

                phone_number = dt.Rows[0]["phone_no"].ToString();
                info = dt.Rows[0]["info"].ToString();
             //   date = Convert.ToDateTime(dt.Rows[0]["date"].ToString());
                pagePhoto = dPhoto.Rows[0]["source"].ToString();
                pageProfilePhoto = pagePhoto;

                
                ViewBag.pageId = pageId;
              

                return RedirectToAction("Index", new { id = pageId });
            }
            return RedirectToAction("Index", "LogIn");
        }
        public ActionResult Index(int id)
        {
            if (isLogged)
            {

                
                pageId = id;
                ViewBag.pageID = pageId;
                //to do set up model
                ViewBag.name = name;
                ViewBag.location = location;
                ViewBag.phone = pagePhone;
           //    ViewBag.date = date;
                ViewBag.phone = phone_number;
                ViewBag.info = info;
                ViewBag.email = pageEmail;
                ViewBag.photo = pageProfilePhoto;
                // get posts 
                DataTable dposts = pageController.getPagePosts(pageId);
                ViewBag.dposts = dposts;
                // get comments for each post

                List<DataTable> comments = new List<DataTable>(); ;

                if (dposts != null)
                {
                    foreach (DataRow row in dposts.Rows)
                    {
                        int pageID = Convert.ToInt32(row["page_id"].ToString());
                        int postId = Convert.ToInt32(row["page_post_id"].ToString());
                        DataTable dc = pageController.getPagePostComments(pageID, postId);
                        comments.Add(dc);

                    }
                }
                ViewBag.comments = comments;

                DataTable devents = pageController.getPageEvents(pageId);
                ViewBag.events = devents;
                return View();
            }
            return RedirectToAction("Index", "LogIn");

        }
        bool validateSignUp(Page page)
        {
            string name = page.name;
            string password = page.password;
            string check_password = page.check_password;
            string phone = page.phone_number;
            string location = page.location;
             
          //  DateTime date = page.date;
            string email = page.email;
            if (password == check_password)
            {
                if (password.Length < 1)
                {
                    ViewBag.message = "Invalid Password! (i.e. lessthan 8 characters)";
                    return false;
                }
                if (phone[0] != '0' || phone[1] != '1')
                {
                    ViewBag.message = "Invalid Phone Number! (i.e. Must be in the format 01xxxxxxxxx, exactly 11 digit)";
                    return false;
                }
                if (location.Length < 3)
                {
                    ViewBag.message = "Invalid Location! (i.e. Must be more than 3 characters)";
                    return false;
                }
           
                if (phone != "" && location != "" && name != "" && password != "")
                {

                    return true;
                }
                else
                {
                    ViewBag.message = "please fill all fields";
                    return false;
                }
            }
            else
            {
                ViewBag.message = "please make sure you entered the same passwords";
                return false;
            }
        }
        [HttpPost]
        public ActionResult InitialSignUp(Page page)
        {
            if (validateSignUp(page))
            {
                ViewBag.Message = "";
                pageEmail = page.email;
                pagePassword = page.password;
                name = page.name;
                location = page.location;
               // date = page.date;
                pagePhone = page.phone_number;
                ViewBag.imageSource = pageProfilePhoto;
                // ViewBag.user = user;
                return View();
            }
            return RedirectToAction("Index", "Signup");

        }

        [HttpPost]
        public ActionResult UploadInitialImage(HttpPostedFileBase file)
        {
            ViewBag.Message = "";
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("../Images/pageProfilePhoto"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    //ViewBag.Message = "image updated successfully";
                    ViewBag.imageSource = "../Images/pageProfilePhoto/" + file.FileName;
                    pageProfilePhoto = "../../Images/pageProfilePhoto/" + file.FileName;
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
        public ActionResult updateInfo(Page page)
        {
            string pageInfo = page.info;
            string imagePath = ViewBag.imageSource;

            if (pageInfo.Length > maxInfoLength)
            {
                ViewBag.Message = "please enter a bio less than 100 characters";
                ViewBag.imageSource = pageProfilePhoto;
                return View("initialSignUp");
            }

            // send to dp

            // insert page to database 
            pageController.insertPage( name, info, location, pageEmail, pagePassword,pagePhone);

            // get pageID
            DataTable dt = pageController.getPageInfo(pageEmail, pagePassword);
            pageId = Convert.ToInt32(dt.Rows[0][0].ToString());
            // insert photo
             pageController.insertPagePhoto(pageId, pageProfilePhoto);
            

            return RedirectToAction("Index", "Login");

        }

        public ActionResult userSetting(int id)
        {
            if (isLogged)
            {
                ViewBag.name = name;
                ViewBag.location = location;

              //  ViewBag.date = date;
                ViewBag.phone = phone_number;
                ViewBag.info = info;
                ViewBag.email = pageEmail;
                ViewBag.photo = pagePhoto;
                ViewBag.image = pageProfilePhoto;
                ViewBag.pageID = pageId;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult userSetting(Page page)
        {
            // validation
            page.check_password = page.password;
            if (validateSignUp(page))
            {
                string name = page.name;
                string password = page.password;
                string phone = page.phone_number;
                string location = page.location;

                DateTime date = page.date;
                string email = page.email;
                string info = page.info;
                //  userController.updateUserInfo(userId, name, bio, gender, phone, location, birth_date, email, password);
            }
            ViewBag.pageID = pageId;

            return RedirectToAction("userSetting", new { id = pageId });
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
                    ViewBag.imageSource = "../Images/pageProfilePhoto/" + file.FileName;
                    pageProfilePhoto = "../../Images/pageProfilePhoto/" + file.FileName;
                    // updae user photo
                    //  userController.UpdateUserPhoto(userId, userProfilePhoto);
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
            ViewBag.pageID = pageId;
            return RedirectToAction("userSetting", new { id = pageId });
        }




        [HttpPost]
        public ActionResult addPost(EventAndPost e, HttpPostedFileBase postPhoto)
        {
            string postText = e.Post.text;
            string postLocation = e.Post.location;
            DateTime date = DateTime.Now;
            // insert post to db 
             pageController.insertPagePost(pageId,  date, postLocation, 0, postText);
            // check if post has photo
            if (postPhoto != null && postPhoto.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("../Images/pagePostPhotos"),
                    Path.GetFileName(postPhoto.FileName));
                    postPhoto.SaveAs(path);
                    //ViewBag.Message = "image updated successfully";
                    // ViewBag.imageSource = "../Images/userProfilePhoto/" + postPhoto.FileName;
                    string postPhotoURL = "../../Images/pagePostPhotos/" + postPhoto.FileName;
                    // get user post ID
                    DataTable dPost = pageController.getLastInsertedPost();
                    int postID = Convert.ToInt32(dPost.Rows[0]["page_post_id"].ToString());
                    // inser post  photo
                    pageController.insertPagePostPhoto(pageId, postID, postPhotoURL);
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

            return RedirectToAction("Index", new { id = pageId });

        }



    
        [HttpPost]
        public ActionResult AddEvent(EventAndPost e, HttpPostedFileBase eventPhoto)
        {
            string eventLocation = e.Event.location;
            int eventPrice = e.Event.price;
            DateTime eventDate = e.Event.date;
            // insert post to db 
            pageController.insertEvent(pageId, eventDate, eventLocation, eventPrice, 0);
            // check if post has photo
            if (eventPhoto != null && eventPhoto.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("../Images/eventPostPhotos"),
                    Path.GetFileName(eventPhoto.FileName));
                    eventPhoto.SaveAs(path);
                    //ViewBag.Message = "image updated successfully";
                    // ViewBag.imageSource = "../Images/userProfilePhoto/" + postPhoto.FileName;
                    string eventPhotoURL = "../../Images/pagePostPhotos/" + eventPhoto.FileName;
                    // get user post ID
                    DataTable dPost = pageController.getLastInsertedEvent();
                    int eventID = Convert.ToInt32(dPost.Rows[0]["event_id"].ToString());
                    // inser post  photo
                    pageController.insertEventPhoto(pageId, eventID, eventPhotoURL);
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

            return RedirectToAction("Index", new { id = pageId });

        }


    }
}