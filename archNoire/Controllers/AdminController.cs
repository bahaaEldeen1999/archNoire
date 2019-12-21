using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using archNoire.Models;
using archNoire.DBControllers;
using System.Data;
namespace archNoire.Controllers
{
    public class AdminController : Controller
    {
        static private int adminID = -1;
        static private AdminDBController adminDBController;
        
        public AdminController()
        {
            adminDBController = new AdminDBController();
        }

        // GET: Admin
        public ActionResult Index()
        {
            ;//dminID = id;
            ;//ViewBag.adminID = adminID;
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminLogin credentials)
        {
            if(adminDBController.checkAdmin(credentials.admin_id, credentials.password))
            {
                adminID = new int();
                adminID = credentials.admin_id;
                return RedirectToAction("Dashboard");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult Logout()
        {
            adminID = -1;
            return RedirectToAction("Index");
        }

        public ActionResult Dashboard()
        
        {
            if(adminID == -1)
            {
                return RedirectToAction("Index");
            }
            Statistics statistics = new Statistics();
            statistics.usersCount = adminDBController.getUsersCount();
            statistics.pagesCount = adminDBController.getPagesCount();
            statistics.userPostsCount = adminDBController.getUsersPostsCount();
            statistics.pagePostsCount = adminDBController.getPagesPostsCount();
            statistics.eventsCount = adminDBController.getEventsCount();

            return View(statistics); 
        }

        public ActionResult Users()
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            List<user_admin> users = new List<user_admin>();
            System.Data.DataTable data = adminDBController.getAllUsers();
            for(int i=0; i<data.Rows.Count; i++)
            {
                user_admin user= new user_admin();
                user.user_id = (int)data.Rows[i]["user_id"];
                user.name = data.Rows[i]["name"].ToString();
                user.bio = data.Rows[i]["bio"].ToString();
                user.gender = data.Rows[i]["gender"].ToString();
                user.phone_no = data.Rows[i]["phone_no"].ToString();
                user.location = data.Rows[i]["location"].ToString();
                user.birth_date = (DateTime)data.Rows[i]["birth_date"];
                user.personal_email = data.Rows[i]["personal_email"].ToString();

                users.Add(user);
            }
            
            return View(users);
        }

        public ActionResult UserFriends(int user_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            // Get user details
            userFriends userFriendsViewModel = new userFriends();
            userFriendsViewModel.friends = new List<user_admin>();

            System.Data.DataTable user = adminDBController.getUser(user_id);
            if(user.Rows.Count == 1)
            {
                userFriendsViewModel.user_id = (int)user.Rows[0]["user_id"];
                userFriendsViewModel.name = user.Rows[0]["name"].ToString();
            }

            // Get user friends
            user_admin friend;
            System.Data.DataTable friendData = new System.Data.DataTable();

            System.Data.DataTable friendsID = adminDBController.getUserFriends(user_id);
            if(friendsID != null && friendsID.Rows.Count > 0)
            {
                for(int i=0; i<friendsID.Rows.Count; i++)
                {
                    friendData = adminDBController.getUser((int)friendsID.Rows[i][0]);

                    friend = new user_admin();
                    friend.user_id = (int)friendData.Rows[0]["user_id"];
                    friend.name = friendData.Rows[0]["name"].ToString();
                    friend.bio = friendData.Rows[0]["bio"].ToString();
                    friend.gender = friendData.Rows[0]["gender"].ToString();
                    friend.phone_no = friendData.Rows[0]["phone_no"].ToString();
                    friend.location = friendData.Rows[0]["location"].ToString();
                    friend.birth_date = (DateTime)friendData.Rows[0]["birth_date"];
                    friend.personal_email = friendData.Rows[0]["personal_email"].ToString();

                    userFriendsViewModel.friends.Add(friend);
                }
            }

            return View(userFriendsViewModel);

        }



        public ActionResult deleteFriend(int user1_id,int user2_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deleteFriend(user1_id, user2_id);
            return RedirectToAction("UserFriends", new {user_id = user1_id });
        }

        public ActionResult UserPosts(int user_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            // Get user details
            userPosts userPostsViewModel = new userPosts();
            System.Data.DataTable user = adminDBController.getUser(user_id);
            if (user.Rows.Count == 1)
            {
                userPostsViewModel.user_id = (int)user.Rows[0]["user_id"];
                userPostsViewModel.name = user.Rows[0]["name"].ToString();
            }

            // Get user posts
            userPostsViewModel.posts = new List<userPost>();
            userPost post;
            System.Data.DataTable posts = adminDBController.getUserPosts(user_id);
            if (posts != null)
            {
                for (int i = 0; i < posts.Rows.Count; i++)
                {
                    post = new userPost();
                    post.user_id = (int)posts.Rows[i]["user_id"];
                    post.post_id = (int)posts.Rows[i]["post_id"];
                    post.text = posts.Rows[i]["text"].ToString();
                    post.date = (DateTime)posts.Rows[i]["date"];
                    post.location = posts.Rows[i]["postLocation"].ToString();
                    post.no_of_likes = (int)posts.Rows[i]["no_of_likes"];


                    userPostsViewModel.posts.Add(post);
                }
            }

            return View(userPostsViewModel);

        }

        

        public ActionResult deletePost(int user_id , int post_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deletePost(user_id, post_id);
            return RedirectToAction("UserPosts", new { user_id = user_id });
        }


        public ActionResult PostDetails(int user_id, int post_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            System.Data.DataTable post = adminDBController.getUserPost(user_id, post_id);

            PostDetails postdetails = new PostDetails();
            postdetails.post = new userPost();
            postdetails.comments = new List<Comment>();
            postdetails.post.name = adminDBController.getUser(user_id).Rows[0]["name"].ToString();
            postdetails.post.user_id = (int)post.Rows[0]["user_id"];
            postdetails.post.post_id = (int)post.Rows[0]["post_id"];
            postdetails.post.text = post.Rows[0]["text"].ToString();
            postdetails.post.date = (DateTime)post.Rows[0]["date"];
            postdetails.post.location = post.Rows[0]["postLocation"].ToString();
            postdetails.post.no_of_likes = (int)post.Rows[0]["no_of_likes"];

            System.Data.DataTable comments = adminDBController.getPostComments(user_id, post_id);

            Comment comment;
            if (comments != null)
            {
                for (int j = 0; j < comments.Rows.Count; j++)
                {
                    comment = new Comment();
                    comment.user_posted_id = (int)comments.Rows[j]["user_posted_id"];
                    comment.post_id = (int)comments.Rows[j]["post_id"];
                    comment.comment_id = (int)comments.Rows[j]["comment_id"];
                    comment.user_commented_id = (int)comments.Rows[j]["user_commented_id"];
                    comment.text = comments.Rows[j]["text"].ToString();
                    comment.no_of_likes = (int)comments.Rows[j]["no_of_likes"];
                    comment.name = adminDBController.getUser(comment.user_commented_id).Rows[0]["name"].ToString();
                    postdetails.comments.Add(comment);
                }
            }

            return View(postdetails);
        }

        public ActionResult DeleteComment(int user_posted_id, int post_id, int comment_id, int user_commented_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deleteComment(user_posted_id, post_id, comment_id, user_commented_id);
            return RedirectToAction("PostDetails", new { user_id = user_posted_id, post_id = post_id });
        }

        public ActionResult EditUser(int user_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            user_admin user = new user_admin();
            System.Data.DataTable data = adminDBController.getUser(user_id);
            if (data.Rows.Count == 1)
            {
                user.user_id = (int)data.Rows[0]["user_id"];
                user.name = data.Rows[0]["name"].ToString();
                user.bio = data.Rows[0]["bio"].ToString();
                user.gender = data.Rows[0]["gender"].ToString();
                user.phone_no = data.Rows[0]["phone_no"].ToString();
                user.location = data.Rows[0]["location"].ToString();
                user.birth_date = (DateTime)data.Rows[0]["birth_date"];
                user.personal_email = data.Rows[0]["personal_email"].ToString();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(user_admin user)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.updateUser(user.user_id, user.name, user.bio, user.gender, user.phone_no, user.location, user.birth_date, user.personal_email);
            return RedirectToAction("Users");
        }


        public ActionResult EditPage(int page_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            page_admin page = new page_admin();
            System.Data.DataTable data = adminDBController.getPage(page_id);
            if(data.Rows.Count == 1)
            {
                page.page_id = (int)data.Rows[0]["page_id"];
                page.name = data.Rows[0]["name"].ToString();
                page.info = data.Rows[0]["info"].ToString();
                page.location = data.Rows[0]["location"].ToString();
                page.business_email = data.Rows[0]["business_email"].ToString();
                page.phone_no = data.Rows[0]["phone_no"].ToString();
            }
            return View(page);
        }

        [HttpPost]
        public ActionResult EditPage(page_admin page)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.updatePage(page.page_id, page.name, page.info, page.location, page.business_email, page.phone_no);
            return RedirectToAction("Pages");
        }

        public ActionResult DeleteUser(int user_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deleteUser(user_id);
            return RedirectToAction("Users");
            
        }

        public ActionResult DeletePage(int page_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deletePage(page_id);
            return RedirectToAction("Pages");
        }

        public ActionResult Pages()
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            List<page_admin> pages = new List<page_admin>();
            System.Data.DataTable data = adminDBController.getAllPages();
            for(int i=0; i<data.Rows.Count; i++)
            {
                page_admin page = new page_admin();
                page.page_id = (int)data.Rows[i]["page_id"];
                page.name = data.Rows[i]["name"].ToString();
                page.info = data.Rows[i]["info"].ToString();
                page.location = data.Rows[i]["location"].ToString();
                page.business_email = data.Rows[i]["business_email"].ToString();
                page.phone_no = data.Rows[i]["phone_no"].ToString();

                pages.Add(page);

            }

            return View(pages);
        }

        public ActionResult PagePosts(int page_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            PagePosts pagePostsModel = new Models.PagePosts();
            pagePostsModel.name = adminDBController.getPage(page_id).Rows[0]["name"].ToString();
            pagePostsModel.page_id = page_id;
            pagePostsModel.posts = new List<pagePost>();

            System.Data.DataTable posts = adminDBController.getPagePosts(page_id);

            pagePost post;

            if(posts != null)
            {
                for (int i = 0; i < posts.Rows.Count; i++) {
                    post = new pagePost();

                    post.page_id = page_id;
                    post.page_post_id = (int)posts.Rows[i]["page_post_id"];
                    post.date = (DateTime)posts.Rows[i]["date"];
                    post.location = posts.Rows[i]["postLocation"].ToString();
                    post.no_of_likes = (int)posts.Rows[i]["no_of_likes"];
                    post.text = posts.Rows[i]["text"].ToString();

                    pagePostsModel.posts.Add(post);
                }
            }

            return View(pagePostsModel);
        }

        public ActionResult PagePostDetails(int page_id, int page_post_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            System.Data.DataTable post = adminDBController.getPagePost(page_id, page_post_id);

            PagePostDetails postdetails = new PagePostDetails();

            postdetails.post = new pagePost();
            postdetails.comments = new List<Comment>();

            postdetails.post.name = adminDBController.getPage(page_id).Rows[0]["name"].ToString();
            postdetails.post.page_id = (int)post.Rows[0]["page_id"];
            postdetails.post.page_post_id = (int)post.Rows[0]["page_post_id"];
            postdetails.post.text = post.Rows[0]["text"].ToString();
            postdetails.post.date = (DateTime)post.Rows[0]["date"];
            postdetails.post.location = post.Rows[0]["postLocation"].ToString();
            postdetails.post.no_of_likes = (int)post.Rows[0]["no_of_likes"];

            System.Data.DataTable comments = adminDBController.getPagePostComments(page_id, page_post_id);

            Comment comment;
            if (comments != null)
            {
                for (int j = 0; j < comments.Rows.Count; j++)
                {
                    comment = new Comment();
                    comment.user_posted_id = (int)comments.Rows[j]["page_id"];
                    comment.post_id = (int)comments.Rows[j]["page_post_id"];
                    comment.comment_id = (int)comments.Rows[j]["page_post_comment_id"];
                    comment.user_commented_id = (int)comments.Rows[j]["user_commented_id"];
                    comment.text = comments.Rows[j]["text"].ToString();
                    comment.no_of_likes = (int)comments.Rows[j]["no_of_likes"];
                    comment.name = adminDBController.getUser(comment.user_commented_id).Rows[0]["name"].ToString();
                    postdetails.comments.Add(comment);
                }
            }

            return View(postdetails);
        }

        public ActionResult deletePagePostComment(int user_commented_id , int page_id , int page_post_id , int page_post_comment_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deletePagePostComment(user_commented_id, page_id, page_post_id, page_post_comment_id);
            return RedirectToAction("PagePostDetails", new { page_id = page_id, page_post_id = page_post_id });
        }

        public ActionResult deletePagePost(int page_id,int page_post_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deletePagePost(page_id, page_post_id);
            return RedirectToAction("PagePosts", new { page_id = page_id });
        }

        public ActionResult PageReviews(int page_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            Models.PageReviews pageReviews = new PageReviews();
            pageReviews.reviews = new List<Review>();
            pageReviews.page_id = page_id;
            pageReviews.name = adminDBController.getPage(page_id).Rows[0]["name"].ToString();

            System.Data.DataTable reviews = adminDBController.getPageReviews(page_id);
            Review review;
            if (reviews != null)
            {
                for (int i = 0; i < reviews.Rows.Count; i++)
                {
                    review = new Review();
                    review.page_id = page_id;
                    review.user_id = (int)reviews.Rows[i]["user_id"];
                    review.name = reviews.Rows[i]["name"].ToString();
                    review.text = reviews.Rows[i]["text"].ToString();

                    pageReviews.reviews.Add(review);
                }
            }
            return View(pageReviews);
        }

        public ActionResult deleteReview(int user_id, int page_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deleteReview(user_id, page_id);
            return RedirectToAction("PageReviews", new { page_id = page_id });
        }

        [HttpPost]
        public ActionResult LogIn(Admin admin)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            string logInEmail = admin.admin_email;
            string logInPassword = admin.password;
           // System.Diagnostics.Debug.WriteLine("emil : ");
            //System.Diagnostics.Debug.WriteLine(logInEmail);

            // to do sql validastion
            return View();
        }

        public ActionResult PageEvents(int page_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            Models.PageEvents pageEvents = new PageEvents();
            pageEvents.events = new List<EventData>();
            pageEvents.page_id = page_id;
            pageEvents.name = adminDBController.getPage(page_id).Rows[0]["name"].ToString();

            System.Data.DataTable events = adminDBController.getPageEvents(page_id);
            EventData @event;
            if(events != null)
            {
                for(int i=0; i<events.Rows.Count; i++)
                {
                    @event = new EventData();
                    @event.page_id = page_id;
                    @event.event_id = (int)events.Rows[i]["event_id"];
                    @event.date = (DateTime)events.Rows[i]["date"];
                    @event.eventLocation = events.Rows[i]["eventLocation"].ToString();
                    @event.ticket_price = (int)events.Rows[i]["ticket_price"];
                    @event.no_of_people_going = (int)events.Rows[i]["no_of_people_going"];

                    pageEvents.events.Add(@event);
                }
            }

            return View(pageEvents);
        }

        public ActionResult deleteEvent(int page_id, int event_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deleteEvent(page_id, event_id);
            return RedirectToAction("PageEvents", new { page_id = page_id });
        }

        public ActionResult Admins()
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            System.Data.DataTable adminsData = adminDBController.getAllAdmins();
            List<Admin> admins = new List<Admin>();

            Admin admin;
            if(adminsData != null)
            {
                for(int i=0; i<adminsData.Rows.Count; i++)
                {
                    admin = new Admin();
                    admin.admin_id = (int)adminsData.Rows[i]["admin_id"];
                    admin.name = adminsData.Rows[i]["name"].ToString();
                    admin.admin_email = adminsData.Rows[i]["admin_email"].ToString();

                    admins.Add(admin);
                }
            }

            return View(admins);
        }


        public ActionResult EditAdmin(int admin_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            System.Data.DataTable adminData = adminDBController.getAdmin(admin_id);

            Admin admin = new Admin();

            if(adminData != null)
            {
                admin.admin_id = admin_id;
                admin.name = adminData.Rows[0]["name"].ToString();
                admin.admin_email = adminData.Rows[0]["admin_email"].ToString();
                admin.password = adminData.Rows[0]["password"].ToString();
            }

            return View(admin);
        }


        [HttpPost]
        public ActionResult EditAdmin(Admin admin)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.updateAdmin(admin.admin_id, admin.name, admin.admin_email, admin.password);
            return RedirectToAction("Admins");
        }

        public ActionResult CreateAdmin()
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult DeleteAdmin(int admin_id)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.deleteAdmin(admin_id);
            return RedirectToAction("Admins");
        }

        [HttpPost]
        public ActionResult CreateAdmin(Admin admin)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            adminDBController.createAdmin(admin.admin_id, admin.name, admin.admin_email, admin.password);
            return RedirectToAction("Admins");
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            if (adminID == -1)
            {
                return RedirectToAction("Index");
            }
            string SignUpEmail = admin.admin_email;
            string SignUpPassword = admin.password;
            string name = admin.name;
            // System.Diagnostics.Debug.WriteLine("emil : ");
            //System.Diagnostics.Debug.WriteLine(logInEmail);

            // to do sql validastion
            return View("index");
        }

       /* public ActionResult UserPostBetween2Times(DateTime date1, DateTime date2)
        {
            if (adminID != -1)
            {
                DataTable dt = adminDBController.getUserPosts(date1, date2);
                ViewBag.userPostsNumber = dt ;
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Index");


        }
*/

    }
}