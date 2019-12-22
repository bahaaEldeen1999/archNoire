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
        static private string userProfilePhoto = "../../Images/userProfilePhoto/defaultIM.jpg";
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
         static PageDBController pageController = new PageDBController();
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
                userProfilePhoto = userPhoto;

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
                ViewBag.photo = userProfilePhoto;
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
                // get events user is going to
                DataTable devents = userController.getEventsUserGoingTo(userId);
                ViewBag.events = devents;

                // get freinds list
                DataTable dfriends = userController.getUserFreinds(userId);
                ViewBag.friends = dfriends;

                // get lis og pages User like
                DataTable dlikedPages= userController.getPagesUserLike(userId);
                ViewBag.likedPages = dlikedPages;



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
            // check email no used
            DataTable duserEmails = userController.getUserByEmail(email);
            if(duserEmails != null )
            {
                // email in use
                return false;
            }
            if (password == check_password)
            {
                if (password.Length < 8)
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
        bool validateSetting(User user)
        {
            string name = user.name;
            string password = user.password;
            string check_password = user.check_password;
            string phone = user.phone_number;
            string location = user.location;
            string gender = user.gender;
            DateTime birth_date = user.birth_date;
            string email = user.email;
          
            if (password == check_password)
            {
                if (password.Length < 8)
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
            // make user befreind himself
            userController.insertUserFriend(userId, userId);

            return RedirectToAction("Index", "Login");

        }

        public ActionResult userSetting(int id)
        {
            if (isLogged)
            {
                ViewBag.name = name;
                ViewBag.location = location;
                ViewBag.gender = gender;
                ViewBag.birthDate = birth_date;
                ViewBag.phone = phone_number;
                ViewBag.bio = bio;
                ViewBag.email = userEmail;
                ViewBag.photo = userPhoto;
                ViewBag.image = userProfilePhoto;
                ViewBag.userID = userId;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult userSetting(User user)
        {
            // validation
            user.check_password = user.password;
            if (validateSetting(user))
            {
                string name1 = user.name;
                string password1 = user.password;
                string phone1 = user.phone_number;
                string location1 = user.location;
                string gender1 = user.gender;
                DateTime birth_date1 = user.birth_date;
                string email1 = user.email;
                string bio1 = user.bio;
                if (userController.updateUserInfo(userId, name1, bio1, gender1, phone1, location1, birth_date1, email1, password1) != 0)
                {

                  
                        name = name1;
                        location =location1;
                        gender = gender1;
                        phone_number =phone1;
                        bio = bio1;
                        birth_date =birth_date1;
        

                    
                }
            }
            ViewBag.userID = userId;

            return RedirectToAction("userSetting", new { id = userId });
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
                    // updae user photo
                    userController.UpdateUserPhoto(userId, userProfilePhoto);
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
            ViewBag.userID = userId;
            return RedirectToAction("userSetting", new { id = userId });
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

                DataTable pagePosts = pageController.getPagePostsUserLike(id);
                ViewBag.pagePost = pagePosts;

                // get comments for each page post

                List<DataTable> pageComments = new List<DataTable>(); ;

                if (pagePosts != null)
                {
                    foreach (DataRow row in pagePosts.Rows)
                    {
                        int pageID = Convert.ToInt32(row["page_id"].ToString());
                        int postId = Convert.ToInt32(row["page_post_id"].ToString());
                        DataTable dc = pageController.getPagePostComments(pageID, postId);
                        pageComments.Add(dc);

                    }
                }
                ViewBag.pageComments = pageComments;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult UserSearched(int id)
        {
            if (isLogged)
            {
                if(userId == id)
                {
                      return RedirectToAction("Index", new { id = userId });
                }
                ViewBag.userSearchedID = id;
                ViewBag.userID = userId;
                // check if friend with him
                DataTable dt = userController.getIfUserIsFriend(userId, id);
                if (dt != null && dt.Rows.Count != 0)
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

                DataTable duser2Posts = userController.getUserPosts(id);
                ViewBag.dposts = duser2Posts;
                // get comments for each post

                List<DataTable> comments = new List<DataTable>(); ;

                if (duser2Posts != null)
                {
                    foreach (DataRow row in duser2Posts.Rows)
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
        public ActionResult SearchPage(User user)
        {

            if (isLogged)
            {
                ViewBag.userID = userId;

                // get users
                DataTable dusers = userController.getUserFromName(user.searchedUser);
                ViewBag.searchedUsers = dusers;
                // get pages 
                DataTable dpages = pageController.getPageByName(user.searchedUser);
                ViewBag.searchedPages = dpages;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult addPost(Post post, HttpPostedFileBase postPhoto)
        {
            string postText = post.text;
            string postLocation = post.location;
            DateTime date =DateTime.Now;
            // insert post to db 
            userController.insertUserPost(userId, postText, date, postLocation, 0);
            // check if post has photo
            if (postPhoto != null && postPhoto.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("../Images/userPostPhotos"),
                    Path.GetFileName(postPhoto.FileName));
                    postPhoto.SaveAs(path);
                    //ViewBag.Message = "image updated successfully";
                   // ViewBag.imageSource = "../Images/userProfilePhoto/" + postPhoto.FileName;
                    string postPhotoURL  = "../../Images/userPostPhotos/" + postPhoto.FileName;
                    // get user post ID
                    DataTable dPost = userController.getLastInsertedPost();
                    int postID = Convert.ToInt32( dPost.Rows[0]["post_id"].ToString());
                    // inser post  photo
                    userController.insertUserPostPhoto(userId, postID, postPhotoURL);
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

        public ActionResult PageSearched(int pageID)
        {
            if (isLogged)
            {
                DataTable dpage = pageController.getPageById(pageID);
                ViewBag.page = dpage;
                ViewBag.userID = userId;
                // check if like with him
                DataTable dt = userController.getIfUserLikePage(userId, pageID);
                if (dt != null && dt.Rows.Count != 0)
                {
                    ViewBag.like = true;
                }
                else
                {
                    ViewBag.like = false;
                }
                // get page posts
                DataTable dpagePosts = pageController.getPagePosts(pageID);
                ViewBag.posts = dpagePosts;
                List<DataTable> comments = new List<DataTable>(); ;


                if (dpagePosts != null)
                {
                    foreach (DataRow row in dpagePosts.Rows)
                    {

                        int postId = Convert.ToInt32(row["page_post_id"].ToString());
                        DataTable dc = pageController.getPagePostComments(pageID, postId);
                        comments.Add(dc);

                    }
                }
                ViewBag.comments = comments;

                DataTable devents = pageController.getPageEvents(pageID);
                ViewBag.events = devents;

                DataTable dreviews = pageController.getPageREview(pageID);
                ViewBag.reviews = dreviews;

                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult likePageComment(int user_liked_id, int postId, int pagePostedID, int userCommentedID, int comment_id)
        {
            if (pageController.insertPagePostCommentLike(user_liked_id, userCommentedID, pagePostedID, postId, comment_id) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = pageController.getNoOfLikesOfPageComment(pagePostedID, postId, comment_id);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);

                pageController.updateCommentNoOfLikes(pagePostedID, postId, comment_id, noOfLikes);
            }
            return RedirectToAction("PageSearched", new { pageID = pagePostedID });
        }

        [HttpPost]
        public ActionResult AddCommentPageSearch(int userID, int postID, int pageID, string text)
        {
            pageController.insertPagePostComment(userID, pageID, postID, text, 0);

            return RedirectToAction("PageSearched", new { pageID = pageID });
        }
        [HttpPost]
        public ActionResult likePostPageSearch(int userId, int postId, int pageID)
        {
            if (pageController.insertPagePostLike(userId, pageID, postId) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = pageController.getNoOfLikesOfPagePost(pageID, postId);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);

                pageController.updatePostNoOfLikes(pageID, postId, noOfLikes);
            }
            return RedirectToAction("PageSearched", new { pageID = pageID });
        }

        [HttpPost]
        public ActionResult AddLikePage(int pageID)
        {
            pageController.insertPageLike(userId, pageID);

            return RedirectToAction("PageSearched", new { pageID = pageID });
        }
        [HttpPost]
        public ActionResult RemoveLikePage(int pageID)
        {
            pageController.DeletePageLike(userId, pageID);

            return RedirectToAction("PageSearched", new { pageID = pageID });
        }

        [HttpPost]
        public ActionResult goingToEvent(int userID, int pageID,int eventID)
        {
            if (userController.insertGoingToEvent(userID, pageID, eventID) != 0)
            {
                // update number of people going to event
                // get the event
                DataTable noOfPeopleGoing = pageController.getNoOfGoingToEvent(pageID, eventID);

                int num = noOfPeopleGoing.Rows.Count;
                pageController.updateEventNoOfLikes(pageID, eventID, num);
            }
            return RedirectToAction("PageSearched", new { pageID = pageID });
        }
        [HttpPost]
        public ActionResult AddReview(int userID, int pageID,string text)
        {
            pageController.insertPageReview(userID, pageID, text);
            return RedirectToAction("PageSearched", new { pageID = pageID });
        }


        [HttpPost]
        public ActionResult likePagePostHome(int userId, int postId, int pagePostedID)
        {
            if (pageController.insertPagePostLike(userId, pagePostedID, postId) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = pageController.getNoOfLikesOfPagePost(pagePostedID, postId);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);

                pageController.updatePostNoOfLikes(pagePostedID, postId, noOfLikes);
            }
            return RedirectToAction("Home", new { id = userId });
        }
        [HttpPost]
        public ActionResult likePageCommentHome(int user_liked_id, int postId, int pagePostedID, int userCommentedID, int comment_id)
        {
            if (pageController.insertPagePostCommentLike(user_liked_id, userCommentedID, pagePostedID, postId, comment_id) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = pageController.getNoOfLikesOfPageComment(pagePostedID, postId, comment_id);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);

                pageController.updateCommentNoOfLikes(pagePostedID, postId, comment_id, noOfLikes);
            }
            return RedirectToAction("Home", new { id = userId });
        }
        [HttpPost]
        public ActionResult AddPageCommentHome(int user_added_comment, int postID, int pagePostedID, string text)
        {
            pageController.insertPagePostComment(user_added_comment, pagePostedID, postID, text, 0);

            return RedirectToAction("Home", new { id = userId });
        }
        [HttpPost]
        public ActionResult LogOut()
        {
            isLogged = false;
            // reset static variables
            userProfilePhoto = "../../Images/userProfilePhoto/defaultIM.jpg";
            userId = 0;
            isLogged = false;
             userEmail = "";
            userPassword = "";
           name = "";
             location = "";
            gender = "";
             birth_date = DateTime.Now;
          phone_number = "";
           bio = "default bio";
           userPhoto = "";

           userController = new UserDBController();
             pageController = new PageDBController();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult likePostUserSearched(int userId, int postId, int userPostedID)
        {
            if (userController.insertUserPostLike(userPostedID, postId, userId) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = userController.getNoOfLikesOfUserPost(userPostedID, postId);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);

                userController.updatePostNoOfLikes(userPostedID, postId, noOfLikes);
            }
            return RedirectToAction("UserSearched", new { id = userPostedID });
        }
        [HttpPost]
        public ActionResult likeCommentUsersearched(int user_liked_id, int postId, int userPostedID, int userCommentedID, int comment_id)
        {
            if (userController.insertUserPostCommentLike(userCommentedID, userPostedID, postId, comment_id, user_liked_id) != 0)
            {
                // update number of likes
                DataTable dNoOfLikes = userController.getNoOfLikesOfUserComment(userPostedID, postId, comment_id);
                int noOfLikes = Convert.ToInt32(dNoOfLikes.Rows.Count);

                userController.updateCommentNoOfLikes(userPostedID, postId, comment_id, noOfLikes);
            }
            return RedirectToAction("UserSearched", new { id = userPostedID });
        }
        [HttpPost]
        public ActionResult AddCommentUserSearched(int userID, int postID, int userPostedID, string text)
        {
            userController.insertUserPostComment(userPostedID, postID, userID, text, 0);

            return RedirectToAction("UserSearched", new { id = userPostedID });
        }
    }
}