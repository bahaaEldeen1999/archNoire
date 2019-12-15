using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using archNoire.Models;
using archNoire.App_Start;
using archNoire.DBControllers;
using System.Data;

namespace archNoire.Controllers
{
    public class UserController : Controller
    {
        private int maxBioLength = 100;
        private string userProfilePhoto = "../../Images/userProfilePhoto/defaultIM.jpg";
        static private int userId ;
        static private bool isLogged = false;
        static private string userEmail;
        static private string userPassword;
        static private string name;
        static private string location;
        static private string gender;
        static private DateTime birth_date;
        static private string phone_number;
        static private string bio;
        static private string userPhoto;

         static UserDBController userController = new UserDBController();
        [HttpPost]
        public ActionResult Index(User user)
        {
            // get userId
            userEmail = user.email;
            userPassword = user.password;
            DataTable dt = userController.getUserInfo(userEmail, userPassword);
            if (dt != null && dt.Rows.Count != 0)
            {
                isLogged = true;
                userId = Convert.ToInt32(dt.Rows[0][0].ToString());

                DataTable dPhoto = userController.getUserPhoto(userId);

               
                name = dt.Rows[0]["name"].ToString();
                location = dt.Rows[0]["location"].ToString();
                gender = dt.Rows[0]["gender"].ToString();
                phone_number = dt.Rows[0]["phone_no"].ToString();
                bio = dt.Rows[0]["bio"].ToString();
                birth_date = Convert.ToDateTime( dt.Rows[0]["birth_date"].ToString());
                userPhoto = dPhoto.Rows[0]["source"].ToString();


                //userId = id;
                //ViewBag.userID = userId;
                //to do set up model

                return RedirectToAction("Index", new { id = userId }); 
            }
            return RedirectToAction("Index", "LogIn");
        }
        public ActionResult Index(int id)
        {
            if (isLogged)
            {
               
                ViewBag.Message = "";
                userId = id;
                ViewBag.userID = userId;
                //to do set up model
                ViewBag.name = name;
                ViewBag.location = location;
                ViewBag.gender = gender;
                ViewBag.birthDate = birth_date;
                ViewBag.phone = phone_number;
                ViewBag.bio = bio;
                ViewBag.email = userEmail;
                ViewBag.photo = userPhoto;
                // get posts 
                DataTable dposts = userController.getUserPosts(userId);
                ViewBag.dposts = dposts;
                // get comments for each post
                
                List<DataTable> comments = new List<DataTable>(); ;

                if (dposts != null)
                {
                    foreach (DataRow row in dposts.Rows)
                    {
                        int userID = Convert.ToInt32(row["user_id"].ToString());
                        int postId = Convert.ToInt32(row["post_id"].ToString());
                        DataTable dc = userController.getPostComments(userID, postId);
                        comments.Add(dc);

                    }
                }
                ViewBag.comments = comments;
                return View();
            }
            return RedirectToAction("Index", "LogIn");

        }
        bool validateSignUp(User user)
        {
            string name =user.name;
            string password = user.password;
            string check_password =user.check_password;
            string phone = user.phone_number;
            string location = user.location;
            string gender = user.gender;
            DateTime birth_date = user.birth_date;
            string email = user.email;
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
                if (birth_date.Year < 1920 || birth_date.Year > 2020 || birth_date.Month < 1 || birth_date.Day < 1)
                {
                    ViewBag.message = "Invalid BirthDate! (i.e. Must be in the format mm/dd/yyyy)";
                    return false;
                }
                if (phone != "" && location != "" && gender != "" && name != "" && password != "")
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
       public ActionResult InitialSignUp(User user)
        {
            if (validateSignUp(user))
            {
                ViewBag.Message = "";
                userEmail = user.email;
                userPassword = user.password;
                name = user.name;
                location = user.location;
                birth_date = user.birth_date;
                phone_number = user.phone_number;
                gender = user.gender;
                ViewBag.imageSource = userProfilePhoto;
                // ViewBag.user = user;
                return View();
            }
            return RedirectToAction("Index", "Signup");

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
                    userProfilePhoto = "../../Images/userProfilePhoto/" + file.FileName; 
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

            // insert user to database 
            userController.insertUser(name,userBio, gender, phone_number, location, birth_date, userEmail, userPassword);

            // get userId
            DataTable dt = userController.getUserInfo(userEmail, userPassword);
            userId = Convert.ToInt32( dt.Rows[0][0].ToString());
            // insert photo
            userController.insertUserPhoto(userId, userProfilePhoto);

            return RedirectToAction("Index", "Login");

        }

        public ActionResult userSetting(int id)
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
                    userProfilePhoto = "../../Images/userProfilePhoto/" + file.FileName;
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

        public ActionResult Home(int id)
        {
            if (isLogged)
            {
                ViewBag.userID = id;
                // get his and his friends posts
                DataTable dposts = userController.getFriendsAndUsersPosts(id);
                ViewBag.dposts = dposts;
                // get comments for each post

                List<DataTable> comments = new List<DataTable>(); ;

                if (dposts != null)
                {
                    foreach (DataRow row in dposts.Rows)
                    {
                        int userID = Convert.ToInt32(row["user_id"].ToString());
                        int postId = Convert.ToInt32(row["post_id"].ToString());
                        DataTable dc = userController.getPostComments(userID, postId);
                        comments.Add(dc);

                    }
                }
                ViewBag.comments = comments;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult UserSearched(int id)
        {
            ViewBag.userSearchedID = id;
            ViewBag.userID = userId;
            // check if friend with him
            DataTable dt = userController.getIfUserIsFriend(userId, id);
            if(dt != null && dt.Rows.Count != 0)
            {
                ViewBag.friend = true;
            }
            else
            {
                ViewBag.friend = false;
            }
            // get user2 info 
            DataTable dUser2 = userController.getUserInfoFromId(id);
            ViewBag.user2 = dUser2;

            return View();
        }
        public ActionResult SearchPage(User user)
        {
            ViewBag.userID = userId;
           // ViewBag.searchName = user.searchedUser;
            //int[] searchIDS = { 1, 2, 3, 4, 6, 7, 8, 9, 2, 21, 12 };
            //ViewBag.usersSearchedID = searchIDS;

            // get users
            DataTable dt = userController.getUserFromName(user.searchedUser);
            ViewBag.searchedUsers = dt;
            return View();
        }
        [HttpPost]
        public ActionResult addPost(Post post)
        {
            string postText = post.text;
            string postLocation = post.location;
            DateTime date = post.date;
            // insert post to db 
            userController.insertUserPost(userId, postText, date, postLocation, 0);
            return RedirectToAction("Index", new { id = userId });

        }

        [HttpPost]
        public ActionResult AddFriend(int user2ID)
        {
            userController.insertUserFriend(userId, user2ID);
            return RedirectToAction("UserSearched", new { id = user2ID });

        }
        [HttpPost]
        public ActionResult RemoveFriend(int user2ID)
        {
            userController.DeleteFriend(userId, user2ID);
            return RedirectToAction("UserSearched", new { id = user2ID });

        }
        [HttpPost]
        public ActionResult likePostWall(int userId,int postId,int userPostedID)
        {
            if (userController.insertUserPostLike(userPostedID, postId, userId) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = userController.getNoOfLikesOfUserPost(userPostedID, postId);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);
               
                userController.updatePostNoOfLikes(userPostedID, postId, noOfLikes);
            }
            return RedirectToAction("Index", new { id = userId });
        }
        [HttpPost]
        public ActionResult likeComment(int user_liked_id, int postId, int userPostedID,int userCommentedID,int comment_id)
        {
            if(userController.insertUserPostCommentLike( userCommentedID,userPostedID, postId,comment_id, user_liked_id) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = userController.getNoOfLikesOfUserComment(userPostedID, postId,comment_id);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);

                userController.updateCommentNoOfLikes(userPostedID, postId, comment_id,noOfLikes);
            }
            return RedirectToAction("Index", new { id = userId });
        }
        [HttpPost]
        public ActionResult AddComment(int userID,int postID,int userPostedID,string text)
        {
            userController.insertUserPostComment(userPostedID, postID, userID, text,0);

            return RedirectToAction("Index", new { id = userId });
        }
        [HttpPost]
        public ActionResult likePostHome(int userId, int postId, int userPostedID)
        {
            if (userController.insertUserPostLike(userPostedID, postId, userId) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = userController.getNoOfLikesOfUserPost(userPostedID, postId);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);

                userController.updatePostNoOfLikes(userPostedID, postId, noOfLikes);
            }
            return RedirectToAction("Home", new { id = userId });
        }
        [HttpPost]
        public ActionResult likeCommentHome(int user_liked_id, int postId, int userPostedID, int userCommentedID, int comment_id)
        {
            if (userController.insertUserPostCommentLike(userCommentedID, userPostedID, postId, comment_id, user_liked_id) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = userController.getNoOfLikesOfUserComment(userPostedID, postId, comment_id);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);

                userController.updateCommentNoOfLikes(userPostedID, postId, comment_id, noOfLikes);
            }
            return RedirectToAction("Home", new { id = userId });
        }
        [HttpPost]
        public ActionResult AddCommentHome(int user_added_comment, int postID, int userPostedID, string text)
        {
            userController.insertUserPostComment(userPostedID, postID, user_added_comment, text, 0);

            return RedirectToAction("Home", new { id = userId });
        }


    }
}